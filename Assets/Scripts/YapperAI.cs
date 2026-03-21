using System;
using UnityEngine;
using UnityEngine.AI;
using TMPro;
using UnityEngine.UIElements;

public class YapperAI : MonoBehaviour
{   
    public enum YapperState
    {
        Scouting,
        Chasing,
        Conversing,
        Satisfied
    }

    [Header("References")]
    public Transform player;
    public Camera playerCamera;
    public PlayerMovement playerMovement;

    [Header("Variables")]
    public float scoutingSpeed = 3f;
    public float chasingSpeed = 8f;
    public float satisfactionDuration = 10f; //Time Yapper remains satisfied before returning to scouting

    [Header("Detection")]
    public float detectionRange = 12f;
    public float losePlayerRange = 18f;
    public float conversationRange = 4f;

    [Header("NavMeshAgent Variables")]
    private NavMeshAgent yapperAgent;
    private YapperState yapperState;

    [Header("Animator Components")]
    public Animator yapperAnimator;
    public bool isScouting;
    public bool isChasing;
    public bool isConversing;
    
    [Header("Dialogue")]
    private String[][] dialogueOptions;
    public TextMeshProUGUI dialogueText;   // Assign your TMP text in Inspector
    public float typeSpeed = 0.1f;     // Delay between letters
    public float displayDuration = 1f;  // How long it stays fully visible
    private string fullText; //Stores complete text to display

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoutingSpeed = 3f;
        chasingSpeed = 8f;
        satisfactionDuration = 10f;
        detectionRange = 12f;
        losePlayerRange = 18f;
        conversationRange = 4f;
        typeSpeed = 0.1f;
        displayDuration = 1f;
        
        yapperAgent = GetComponent<NavMeshAgent>(); //Obtain NavMeshAgent component from yap
        yapperAgent.speed = scoutingSpeed;
        yapperAgent.angularSpeed = 120f; //Set angular speed for smoother turning
        yapperState = YapperState.Scouting; //Set scouting as initial state

        dialogueOptions = new String[][]
        {
            new String[] // Dialogue set 1
            {
                "Sup…  just wanted to discuss your car’s extended warranty." ,
                "Oh, wait, you don’t have a car?" ,
                "You only have that moped?" ,
                "..." ,
                "Well, do you wanna discuss your moped’s extended warranty?"
            },
            new String[] // Dialogue set 2
            {
                "Hey!! Hi!! I just really wanted to talk to you!!! Cause like… wow! You're still a human?" ,
                "O-M-G! Like– How???" ,
                "Do you just smell that badly that no one wants to eat you?" ,
                "KIDDING!!!! ish. :P"
            },
            new String[] // Dialogue set 3
            {
                "Hey." ,
                "Oh wow. You’re not a zombie yet." ,
                "Damn… I remember when I was still human." ,
                "Thank God I'm not anymore. My workplace was going to shit, so there was a chance of me getting laid off."
            },
        };

        fullText = dialogueText.text;  // Cache the original text
        dialogueText.text = "";        // Hide the text at start
        dialogueText.gameObject.SetActive(false); //Disable text object until triggered
    }

    // Update is called once per frame
    void Update()
    {
        //handles AI type behavior based on current state
        Movement(yapperState);
        switch (yapperState)
        {
            case YapperState.Scouting:     Scouting();     break;
            case YapperState.Chasing:      Chasing();      break;
            case YapperState.Conversing:   Conversing();   break;
            case YapperState.Satisfied:    Satisfied();    break;
        }
    }

    void Scouting()
    {
        if (Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            ChangeState(YapperState.Chasing);
            Debug.Log("Yapper has detected the player and is now chasing.");
        }
    }

    void Chasing()
    {
        if (Vector3.Distance(transform.position, player.position) > losePlayerRange)
        {
            ChangeState(YapperState.Scouting);
            Debug.Log("Yapper has lost the player and is now scouting.");
        }
        else if (Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(player.position.x, 0, player.position.z)) <= conversationRange) //Assuming 2 units is close enough to converse
        {
            ChangeState(YapperState.Conversing);
            Debug.Log("Yapper is close enough to converse with the player.");
        }
    }

    void Conversing()
    {
        
    }

    void Satisfied()
    {
        
    }

    void ChangeState(YapperState newState)
    {
        yapperState = newState;

        switch (newState)
        {
            case YapperState.Scouting:

                yapperAnimator.SetBool("isScouting", true);
                yapperAnimator.SetBool("isChasing", false);
                yapperAnimator.SetBool("isConversing", false);

                yapperAgent.speed = scoutingSpeed;
                yapperAnimator.speed = yapperAgent.speed / 2f; //Adjust animation speed based on movement speed
                yapperState = YapperState.Scouting;
                break;

            case YapperState.Chasing:

                yapperAnimator.SetBool("isScouting", false);
                yapperAnimator.SetBool("isChasing", true);
                yapperAnimator.SetBool("isConversing", false);

                yapperAgent.speed = chasingSpeed;
                yapperState = YapperState.Chasing;
                break;

            case YapperState.Conversing:

                yapperAnimator.SetBool("isScouting", false);
                yapperAnimator.SetBool("isChasing", false);
                yapperAnimator.SetBool("isConversing", true);

                yapperState = YapperState.Conversing;
                yapperAnimator.speed = 1f;
                playerMovement.LockPlayerMovement(true); //Lock player movement while conversing
                initiateDialogue();
                break;

            case YapperState.Satisfied:

                yapperAnimator.SetBool("isScouting", false);
                yapperAnimator.SetBool("isChasing", false);
                yapperAnimator.SetBool("isConversing", false);

                yapperState = YapperState.Satisfied;
                yapperAgent.speed = scoutingSpeed;
                yapperAnimator.speed = 1f;
                playerMovement.LockPlayerMovement(false); //Unlock player movement when satisfied
                break;
        }
    }

    private void ReturnToScouting()
    {
        ChangeState(YapperState.Scouting);
        Debug.Log("Yapper has returned to scouting after being satisfied.");
    }

    void Movement(YapperState state)
    {
        NavMeshHit hit;
        Vector3 randomDirection = new Vector3(0, 0, 0);

        switch (state)
        {
            case YapperState.Scouting:

                if (!yapperAgent.pathPending && yapperAgent.remainingDistance < 0.5f)
                    {
                    randomDirection = UnityEngine.Random.insideUnitSphere * 20f; //Random direction within a radius of 20 units
                    randomDirection += transform.position; //Checks if movement is within NavMesh boundaries before moving the target
                    if (NavMesh.SamplePosition(randomDirection, out hit, 20f, NavMesh.AllAreas))
                    {
                        yapperAgent.SetDestination(hit.position); //Set the destination for the NavMeshAgent                
                    }
                }
                break;

            case YapperState.Chasing:

                yapperAgent.SetDestination(player.position); //Set the destination for the NavMeshAgent to the player's position
                break;

            case YapperState.Conversing:

                yapperAgent.SetDestination(transform.position); //Freeze the yapper in place while conversing
                playerCamera.transform.LookAt(new Vector3(transform.position.x, transform.position.y, transform.position.z)); //Make the player's camera face the Yapper while conversing
                Vector3 lookAtTarget = new Vector3(player.position.x, transform.position.y, player.position.z); //Create a target position that has the player's X and Z but the Yapper's Y
                transform.LookAt(lookAtTarget); //Make the Yapper look at the target position
                break;

            case YapperState.Satisfied:

                randomDirection = UnityEngine.Random.insideUnitSphere * 20f; //Random direction within a radius of 20 units
                randomDirection += transform.position; //Checks if movement is within NavMesh boundaries before moving the target
                if (NavMesh.SamplePosition(randomDirection, out hit, 20f, NavMesh.AllAreas))
                {
                    yapperAgent.SetDestination(hit.position); //Set the destination for the NavMeshAgent                
                }
                break;
        }
    }

    void initiateDialogue()
    {
        dialogueText.gameObject.SetActive(true);
        
        int randomSetIndex = UnityEngine.Random.Range(0, dialogueOptions.Length);
        String[] selectedDialogueSet = dialogueOptions[randomSetIndex];
        StartCoroutine(DisplayDialogueSequence(selectedDialogueSet));
    }

    private System.Collections.IEnumerator DisplayDialogueSequence(String[] dialogueSet)
    {        
        foreach (String line in dialogueSet)
        {            
            dialogueText.text = "";
            foreach (char c in line)
            {   
                dialogueText.text += c;             
                yield return new WaitForSeconds(typeSpeed);
            }
            yield return new WaitForSeconds(displayDuration);
        }
        ChangeState(YapperState.Satisfied); // Transition to satisfied state after dialogue sequence
        Invoke("ReturnToScouting", satisfactionDuration); // Schedule return to scouting after satisfaction duration
        dialogueText.gameObject.SetActive(false);
    }
}