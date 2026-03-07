using UnityEngine;

public class FoodDelivery : MonoBehaviour
{   
    public Camera playerCamera; //Reference to player's camera
    public PickupFood player; //Reference to player's PickupFood script to access the currentFood player is holding
    public float raycastDistance = 8f; //Max distance of raycast
    public string[] validHouseTags; //Array to hold all house tags that can receive deliveries

    void Update()
    {   
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * raycastDistance, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance))
        {
            foreach (string tag in validHouseTags)
            {
                if (hit.collider.CompareTag(tag))
                {   
                    //Debug.Log("House Spotted. Press 'F' to deliver food.");
                    
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        if (player.currentFood !=null)
                        {
                            if (player.currentFood.destinationHouseTag == hit.collider.tag)
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
                        else
                        {
                            Debug.Log("You are not holding any food.");
                        }
                    }
                }
            }
        }
    }

    /*private void OnTriggerEnter(Collider other)
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
    }*/
}