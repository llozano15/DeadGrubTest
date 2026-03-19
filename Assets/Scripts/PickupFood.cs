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
        if (currentFood.tag == "House_One") //Done
        {
            houseClueDescription.text = "I got a pile of zombies outside.";
            //Debug.Log("House One");
            return;
        }

        if (currentFood.tag == "House_Two") //Done
        {
            houseClueDescription.text = "I got a red and pink-ish house :P Also, my house is number two!";
            //Debug.Log("House Two");
            return;
        }

        if (currentFood.tag == "House_Three") //Done
        {
            houseClueDescription.text = "I got a dog outside, if you see him there, that's my house (p.s you can say hi to peanut, he doesn’t bite.)";
            //Debug.Log("House Three");
            return;
        }

        if (currentFood.tag == "House_Four") //Done
        {
            houseClueDescription.text = "There’s someone eating someone else outside my yard. If you see that, that's my house.";
            //Debug.Log("House Four");
            return;
        }

        if (currentFood.tag == "House_Five") //Done
        {
            houseClueDescription.text = "House five. Just leave it in front.";
            //Debug.Log("House Five");
            return;
        }

        if (currentFood.tag == "House_Six") //Done
        {
            houseClueDescription.text = "House six! Just leave it in front! :)";
            //Debug.Log("House Six");
            return;
        }

        if (currentFood.tag == "House_Seven") //Done
        {
            houseClueDescription.text = "I got someone impaled on my fence.";
            //Debug.Log("House Seven");
            return;
        }

        if (currentFood.tag == "House_Eight") //Done
        {
            houseClueDescription.text = "My house number is 3 + 5 :)";
            //Debug.Log("House Eight");
            return;
        }

        if (currentFood.tag == "House_Nine") //Done
        {
            houseClueDescription.text = "Dave is outside.";
            //Debug.Log("House Nine");
            return;
        }

        if (currentFood.tag == "House_Ten") //Done
        {
            houseClueDescription.text = "My house is the one next to where Dave is standing.Left side.";
            //Debug.Log("House Ten");
            return;
        }

        if (currentFood.tag == "House_Eleven") //Done
        {
            houseClueDescription.text = "Someone is like… on fire, in front of my house… just ignore’em.";
            //Debug.Log("House Eleven");
            return;
        }

        if (currentFood.tag == "House_Twelve") //Done
        {
            houseClueDescription.text = "Hi!!! Just leave it in front plz!!!! House 12! Thx uuuu!! ^w^";
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
