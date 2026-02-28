using UnityEngine;

public class PickupZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PickupFood player = other.GetComponent<PickupFood>();

        if (player != null)
        {
            player.isInPickupZone = true;
        }

        Debug.Log("Player entered pickup zone.");
    }

    private void OnTriggerExit(Collider other)
    {
        PickupFood player = other.GetComponent<PickupFood>();

        if (player != null)
        {
            player.isInPickupZone = false;
        }

        Debug.Log("Player exited pickup zone.");
    }
}