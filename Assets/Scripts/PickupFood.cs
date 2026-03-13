using UnityEngine;
using TMPro;
using System.Data.Common;
using Unity.VisualScripting;

public class PickupFood : MonoBehaviour
{
    public PickupZone pickupZone;
    public Food currentFood = null;
    public TextMeshProUGUI houseClueDescription;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentFood != null)
        {
            //Debug.Log("FOOD");
            UpdateHouseClueDescription();
        }
        else
        {
            //Debug.Log("Not holding any food");
            houseClueDescription.text = "No Food to Deliver";
        }

        if (pickupZone != null && pickupZone.isInPickupZone && currentFood == null)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                FoodPickup();
            }

            //Debug.Log("Player is in pickup zone. Press 'F' to pick up food.");
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

    void UpdateHouseClueDesc2()
    {

    }

    void UpdateHouseClueDescription()
    {
        if (currentFood.tag == "House_One")
        {
            houseClueDescription.text = "1";
            //Debug.Log("House One");
            return;
        }

        if (currentFood.tag == "House_Two")
        {
            houseClueDescription.text = "2";
            //Debug.Log("House Two");
            return;
        }

        if (currentFood.tag == "House_Three")
        {
            houseClueDescription.text = "3";
            //Debug.Log("House Three");
            return;
        }

        if (currentFood.tag == "House_Four")
        {
            houseClueDescription.text = "4";
            //Debug.Log("House Four");
            return;
        }

        if (currentFood.tag == "House_Five")
        {
            houseClueDescription.text = "5";
            //Debug.Log("House Five");
            return;
        }

        if (currentFood.tag == "House_Six")
        {
            houseClueDescription.text = "6";
            //Debug.Log("House Six");
            return;
        }

        if (currentFood.tag == "House_Seven")
        {
            houseClueDescription.text = "7";
            //Debug.Log("House Seven");
            return;
        }

        if (currentFood.tag == "House_Eight")
        {
            houseClueDescription.text = "8";
            //Debug.Log("House Eight");
            return;
        }

        if (currentFood.tag == "House_Nine")
        {
            houseClueDescription.text = "9";
            //Debug.Log("House Nine");
            return;
        }

        if (currentFood.tag == "House_Ten")
        {
            houseClueDescription.text = "10";
            //Debug.Log("House Ten");
            return;
        }

        if (currentFood.tag == "House_Eleven")
        {
            houseClueDescription.text = "11";
            //Debug.Log("House Eleven");
            return;
        }

        if (currentFood.tag == "House_Twelve")
        {
            houseClueDescription.text = "12";
            //Debug.Log("House Twelve");
            return;
        }

        if (currentFood.tag == "Untagged")
        {
            houseClueDescription.text = "Untagged";
            //Debug.Log("Untagged Food");
            return;
        }
        else
        {
            houseClueDescription.text = "Unknown Food";
            //Debug.Log("Unknown Food");
        }
    }
}
