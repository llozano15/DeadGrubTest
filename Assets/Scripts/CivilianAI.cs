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
    public float idleProbability = 0.03f;
    public float idleDuration = 0.01f;

    [Header("NavMeshAgent Variables")]
    private NavMeshAgent civilianAgent;
    private CivilianStates currentState;

    private int frameCounter = 0;

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
        if (frameCounter % 120 == 0) 
            CivilianBehavior(); //Call method to handle civilian behavior based on current state
        
        // CivilianBehavior();
    }

    void Wandering()
    {
        currentState = CivilianStates.Wandering;
        Debug.Log("Civilian is wandering");
    }

    void Idle()
    {
        currentState = CivilianStates.Idle;
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
    //copilot I revised it to have methods that would determine the current state, but my civilian ai gamobject (which is a zombie) is still not moving. I also added a navmesh agent component to the civilian ai gameobject, but it still does not move. Do you have any suggestions on how to fix this?
    //Make sure the NavMeshAgent component is properly configured (e.g., speed, acceleration, angular speed) and that the NavMesh is baked correctly in your scene. Additionally, check if there are any obstacles or colliders that might be blocking the civilian's movement. You can also add debug logs to see if the wandering behavior is being triggered and if the destination is being set correctly.
    //can you show me where to add the debug logs to check is everything is working correctly?
    //Certainly! You can add debug logs in the Wandering() method to check if the wandering behavior is being triggered and if the destination is being set correctly. Here's an example of where to add the debug logs:

}   
