using UnityEngine;
using TMPro;

public class HouseClues : MonoBehaviour
{   
    public TextMeshProUGUI houseClueDescription;
    public PickupFood player; //Reference to player's PickupFood script to access the currentFood player is holding

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHouseClueDescription();
    }

    void UpdateHouseClueDescription()
    {
        if (player.currentFood != null)
        {
            if (player.currentFood.CompareTag("House_One") )
            {
                houseClueDescription.text = "1";
            }

            if (player.currentFood.CompareTag("House_Two") )
            {
                houseClueDescription.text = "2";
            }
            
            if (player.currentFood.CompareTag("House_Three") )
            {
                houseClueDescription.text = "3";
            }

            if (player.currentFood.CompareTag("House_Four") )
            {
                houseClueDescription.text = "4";
            }

            if (player.currentFood.CompareTag("House_Five") )
            {
                houseClueDescription.text = "5";
            }

            if (player.currentFood.CompareTag("House_Six") )
            {
                houseClueDescription.text = "6";
            }

            if (player.currentFood.CompareTag("House_Seven") )
            {
                houseClueDescription.text = "7";
            }

            if (player.currentFood.CompareTag("House_Eight") )
            {
                houseClueDescription.text = "8";
            }

            if (player.currentFood.CompareTag("House_Nine") )
            {
                houseClueDescription.text = "9";
            }

            if (player.currentFood.CompareTag("House_Ten") )
            {
                houseClueDescription.text = "10";
            }

            if (player.currentFood.CompareTag("House_Eleven") )
            {
                houseClueDescription.text = "11";
            }

            if (player.currentFood.CompareTag("House_Twelve") )
            {
                houseClueDescription.text = "12";
            }
        }
        else
        {
            houseClueDescription.text = "No Food to Deliver";
        }
    }
}
