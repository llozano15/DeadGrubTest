using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    public float walkSpeed; //Movement speed variables
    public Transform orientation; //Reference to the player's orientation
    public GameObject Moped; //Reference to moped game object to transform its rotation with direction player is moving

    //Input variables for horizontal & vertical movement
    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection; //Direction player will move in
    Rigidbody rb; //Reference to player rigidbody
    Rigidbody mopedRb; //Reference to moped rigidbody

    public bool lockPlayer = false; //Variable to lock player movement when delivering food to correct house

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Get reference to player rigidbody component and freeze rotation
        //Freeze rotation prevents player from tipping over upon collision
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        mopedRb = Moped.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {   
        //Call PlayerInput function in FixedUpdate to ensure consistent movement regardless of frame rate
        PlayerInput();
    }

    private void PlayerInput()
    {   
        //Get input for horizontal & vertical movement
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        //Calculate movement direction based on player's orientation and input
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if(!lockPlayer) {
            //Move player in calculated direction at specified speed
            rb.MovePosition(transform.position + moveDirection.normalized * walkSpeed * Time.fixedDeltaTime);
    
            //Rotate moped to face movement direction of player
            if (moveDirection != Vector3.zero)
            {
                Vector3 right = moveDirection.normalized;
                Vector3 up = Vector3.up;
                Vector3 forward = Vector3.Cross(right, up);
                Quaternion targetRotation = Quaternion.LookRotation(forward, up);
                Moped.transform.rotation = Quaternion.Slerp(Moped.transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);
            }
        }
    }

    public void LockPlayerMovement(Boolean setting)
    {
        lockPlayer = setting;
    }
}
