using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{   
    public static GameManager Instance; //Singleton instance to allow other scripts to easily access the GameManager
    
    [Header("FoodTags")]
    public Food[] foodItems; // Array to hold references to all food items in the scene
    private string[] houseTags = { "House_One", "House_Two", "House_Three", "House_Four", "House_Five", "House_Six", "House_Seven", "House_Eight", "House_Nine", "House_Ten", "House_Eleven", "House_Twelve"};

    [Header("GameStateConditions")]
    public bool gameEnded = false;
    public int totalDeliveries = 4; //Max number of deliveries player needs to make
    private int deliveriesLeft; //Keeps track of deliveries left to be made

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
        //Call function at start to randomize tags of food items
        AssignRandomDestinations();
        //Set deliveriesLeft variable = 4
        deliveriesLeft = totalDeliveries;
    }

    void AssignRandomDestinations()
    {
        foreach (Food food in foodItems)
        {
            int randomIndex = Random.Range(0, houseTags.Length);
            food.destinationHouseTag = houseTags[randomIndex];
            food.tag = food.destinationHouseTag; // Set the food's tag to match its destination house tag

            Debug.Log($"Assigned {food.name} to {food.destinationHouseTag}");
            Debug.Log("{food.name} to:" + food.gameObject.tag);
            //Debug.Log(food.name + " -> Deliver to: " + food.destinationHouseTag);
        }
    }

    public void DeliveryCompleted()
    {
        deliveriesLeft--;

        if(deliveriesLeft <= 0)
        {   
            //Triggers EndGame() function
            //Function will check for current rating & show appropriate ending scene
            EndGame(); 
        }
    }

    //Ending game function for when player runs out of time
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void EndGame()
    {
        gameEnded = true;
        float rating = PercentageRatingManager.Instance.maxRating;

        if (rating >= 90f )
            SceneManager.LoadScene("WinScene");
        else if (rating >= 70f)
            SceneManager.LoadScene("NeutralScene");
        else 
            SceneManager.LoadScene("LoseScene");
    }
}
