using UnityEngine;

public class PickupZone : MonoBehaviour
{   
    public bool isInPickupZone = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInPickupZone = true;
            Debug.Log("Player entered pickup zone");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInPickupZone = false;
            Debug.Log("Player left pickup zone");
        }
    }
}