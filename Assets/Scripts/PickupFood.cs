using UnityEngine;

public class PickupFood : MonoBehaviour
{   
    public bool isInPickupZone = false;
    public Food currentFood = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInPickupZone && currentFood == null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                FoodPickup();
            }

            Debug.Log("Player is in pickup zone. Press 'F' to pick up food.");
        }
    }

    void FoodPickup()
    {
        Food availableFood = Object.FindFirstObjectByType<Food>();
        if (availableFood != null && availableFood.gameObject.activeSelf)
        {
            currentFood = availableFood;
            currentFood.gameObject.SetActive(false);
            
            Debug.Log("Picked up: " + currentFood.name);
        }
    }
}
