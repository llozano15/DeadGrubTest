using UnityEngine;

public class FoodDelivery : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PickupFood player = other.GetComponent<PickupFood>();

        if (player != null && player.currentFood != null)
        {
            if (player.currentFood.destinationHouseTag == this.tag)
            {
                Debug.Log("Correct Delivery!");

                Destroy(player.currentFood.gameObject);
                player.currentFood = null;
            }
            else
            {
                Debug.Log("Wrong House!");
            }
        }
    }
}