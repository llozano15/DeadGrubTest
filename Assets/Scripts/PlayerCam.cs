using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCam : MonoBehaviour
{   
    //Float variables for X & Y axis sensitivity 
    public float sensitivityX;
    public float sensitivityY;

    //Reference to the player's orientation
    public Transform orientation;

    //Float variables for X & Y rotation of camera
    private float xRotation;
    private float yRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        //Locks cursor to center of screen & make it invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //Get mouse input for X & Y axis and multiply by sensitivity & deltaTime
        float mouseX = Input.GetAxis("Mouse X") * sensitivityX * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivityY * Time.deltaTime;

        //Add/Subtract mouse input to X & Y rotation variables
        xRotation -= mouseY;
        yRotation += mouseX;

        //Clamp X rotation to prevent camera from flipping over
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        //Rotate camera based on X & Y rotation variables
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);

        //Rotate player's orientation based on Y rotation variable
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}
