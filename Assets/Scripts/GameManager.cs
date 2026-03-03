using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class GameManager : MonoBehaviour
{   
    public static GameManager Instance; //Singleton instance to allow other scripts to easily access the GameManager
    
    [Header("FoodTags")]
    public Food[] foodItems; // Array to hold references to all food items in the scene
    private string[] houseTags = { "House_One", "House_Two", "House_Three", "House_Four", "House_Five", "House_Six", "House_Seven", "House_Eight", "House_Nine", "House_Ten", "House_Eleven", "House_Twelve"};

    //Method is called only one time when script is loaded before Start()
    void Awake()
    {
        //Singleton Pattern
        //Makes sure only one GameManager exists
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("GameManager Instance Set");
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
