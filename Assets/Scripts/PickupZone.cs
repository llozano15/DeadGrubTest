using UnityEngine;

public class PickupZone : MonoBehaviour
{   
    public bool isInPickupZone = false;

    private void OnTriggerEnter(Collider other)
    {
        PickupFood player = other.GetComponent<PickupFood>();

        if (player != null)
        {
            player.pickupZone = this;
            isInPickupZone = true;
        }

        Debug.Log("Player entered pickup zone.");
    }

    private void OnTriggerExit(Collider other)
    {
        PickupFood player = other.GetComponent<PickupFood>();

        if (player != null)
        {
            player.pickupZone = null;
            isInPickupZone = false;
        }

        Debug.Log("Player exited pickup zone.");
    }
}