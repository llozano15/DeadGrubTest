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
    public float minWanderSpeed = 4f;
    public float maxWanderSpeed = 6f;

    [Header("Idle Settings")]
    public float idleProbability = 0.3f;
    public float idleDuration = 5f;

    [Header("NavMeshAgent Variables")]
    private NavMeshAgent civilianAgent;
    private CivilianStates currentState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        civilianAgent = GetComponent<NavMeshAgent>(); //Obtain NavMeshAgent component from civilian AI GameObject
        civilianAgent.speed = UnityEngine.Random.Range(minWanderSpeed, maxWanderSpeed); //Assign a random speed (4f to 6f) to each civilian AI
        currentState = CivilianStates.Wandering; //Set wandering as initial state
    }

    // Update is called once per frame
    void Update()
    {
        CivilianBehavior();
    }

    void CivilianBehavior()
    {   
        //Randomly triggers idle behavior based on idleProbability
        if (currentState == CivilianStates.Wandering && UnityEngine.Random.value < idleProbability)
        {
            currentState = CivilianStates.Idle;
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
            }
        }

        if (currentState == CivilianStates.Idle)
        {
            civilianAgent.SetDestination(transform.position); //Stop moving by setting destination to current position
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
