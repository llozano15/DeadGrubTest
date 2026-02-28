using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public Food[] foodItems; // Array to hold references to all food items in the scene
    private string[] houseTags = { "House_One", "House_Two", "House_Three", "House_Four", "House_Five", "House_Six", "House_Seven", "House_Eight", "House_Nine", "House_Ten", "House_Eleven", "House_Twelve"};

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AssignRandomDestinations();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void AssignRandomDestinations()
    {
        foreach (Food food in foodItems)
        {
            int randomIndex = Random.Range(0, houseTags.Length);
            food.destinationHouseTag = houseTags[randomIndex];

            Debug.Log($"Assigned {food.name} to {food.destinationHouseTag}");
            //Debug.Log(food.name + " -> Deliver to: " + food.destinationHouseTag);
        }
    }
}
