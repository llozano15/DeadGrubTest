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
    }

    private void OnTriggerExit(Collider other)
    {
        PickupFood player = other.GetComponent<PickupFood>();

        if (player != null)
        {
            player.isInPickupZone = false;
        }
    }
}