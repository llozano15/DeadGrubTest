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
    
    [Header("Dialogue")]
    private String[][] dialogueOptions;
    public TextMeshProUGUI dialogueText;   // Assign your TMP text in Inspector
    public float typeSpeed = 0.1f;     // Delay between letters
    public float displayDuration = 1f;  // How long it stays fully visible
    private string fullText; //Stores complete text to display

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        yapperAgent = GetComponent<NavMeshAgent>(); //Obtain NavMeshAgent component from yap
        yapperAgent.speed = scoutingSpeed; //Assign a random speed to each civilian AI
        yapperAgent.angularSpeed = 120f; //Set angular speed for smoother turning
        yapperState = YapperState.Scouting; //Set scouting as initial state

        dialogueOptions = new String[][]
        {
            new String[] // Dialogue set 1
            { 
                "1111111111111111111111" ,
                "2222222222222222222222" ,
                "3333333333333333333333"
            },
            new String[] // Dialogue set 2
            {
                "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" ,
                "BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB" ,
                "CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC" 
            },
            new String[] // Dialogue set 3
            {
                "ashdjfklahsdjkflahsjkdlafsdfa" ,
                "uqiryewuiqroyewiuqyorueiwqoryeuiwqorew" ,
                "qhguiqohurihqeuriqwphe crazy dave stuff fhuiaosdhfuiaodhfuiaofhsduiaofs have you seen my taco" 
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

                yapperState = YapperState.Scouting;
                yapperAgent.speed = scoutingSpeed;
                break;

            case YapperState.Chasing:

                yapperState = YapperState.Chasing;
                yapperAgent.speed = chasingSpeed;
                break;

            case YapperState.Conversing:

                yapperState = YapperState.Conversing;
                playerMovement.LockPlayerMovement(true); //Lock player movement while conversing
                initiateDialogue();
                break;

            case YapperState.Satisfied:

                yapperState = YapperState.Satisfied;
                yapperAgent.speed = scoutingSpeed;
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