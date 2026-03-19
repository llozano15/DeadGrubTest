using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class CivilianAI : MonoBehaviour
{   
    public enum CivilianStates
    {
        Wandering,
        Idle
    }

    [Header("Components & References")]
    //public Transform player;

    [Header("Wandering Variables")]
    public float minWanderSpeed = 2f;
    public float maxWanderSpeed = 4f;

    [Header("Idle Settings")]
    public float idleProbability = 0.2f;
    public float idleDuration = 5f;

    [Header("NavMeshAgent Variables")]
    private NavMeshAgent civilianAgent;
    private CivilianStates currentState;

    private int frameCounter = 0;

    [Header("Animator")]
    private Animator civilianAnimator;
    private bool isWandering = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        civilianAgent = GetComponent<NavMeshAgent>(); //Obtain NavMeshAgent component from civilian AI GameObject
        civilianAnimator = GetComponent<Animator>(); //Obtain Animator component from civilian AI GameObject
        civilianAgent.speed = UnityEngine.Random.Range(minWanderSpeed, maxWanderSpeed); //Assign a random speed (4f to 6f) to each civilian AI
        civilianAgent.angularSpeed = 120f; //Set angular speed for smoother turning
        currentState = CivilianStates.Wandering; //Set wandering as initial state
        civilianAnimator.speed = civilianAgent.speed / maxWanderSpeed; //Adjust animation speed based on movement speed
        CivilianBehavior(); //Call method to handle wandering behavior
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == CivilianStates.Wandering)
        {
            if (!civilianAgent.pathPending && civilianAgent.remainingDistance < 0.5f)
            {
                CivilianBehavior(); //Call method to handle wandering behavior when destination is reached
            }
        }
    }

    void Wandering()
    {
        currentState = CivilianStates.Wandering;
        civilianAnimator.SetBool("isWandering", true);

        civilianAnimator.speed = civilianAgent.speed / maxWanderSpeed; //Adjust animation speed based on movement speed
        Debug.Log("Civilian is wandering");
    }

    void Idle()
    {
        currentState = CivilianStates.Idle;
        civilianAnimator.SetBool("isWandering", false);

        Debug.Log("Civilian is idle");
    }

    void CivilianBehavior()
    {   
        //Randomly triggers idle behavior based on idleProbability
        if (currentState == CivilianStates.Wandering && UnityEngine.Random.value < idleProbability)
        {
            Idle(); //Switch to idle state
            Invoke("Wandering", idleDuration); //Return to wandering after idleDuration seconds
        }

        //Handles wandering & idle behavior of civilian AI
        if (currentState == CivilianStates.Wandering)
        {   
            //Set random destination for wandering behavior
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * 10f; //Random direction within a radius of 10 units
            randomDirection += transform.position; //Checks if movement is within NavMesh boundaries before moving the target
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, 10f, NavMesh.AllAreas))
            {
                civilianAgent.SetDestination(hit.position); //Set the destination for the NavMeshAgent
                Debug.Log("Civilian is wandering to: " + hit.position);
            }
            else
            {
                Debug.Log("No valid NavMesh position found for wandering.");
            }
        }

        if (currentState == CivilianStates.Idle)
        {
            civilianAgent.SetDestination(transform.position); //Stop moving by setting destination to current position
            Debug.Log("Civilian is idle at: " + transform.position);
        }
    }

     private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PercentageRatingManager.Instance.CivilianCollision(); //Call method to reduce rating when player collides with civilian
            Debug.Log("Player collided with civilian");
        }
    }
   
}   
