using UnityEngine;

public class PlayerMovement : MonoBehaviour
{   
    public float walkSpeed; //Movement speed variables
    public Transform orientation; //Reference to the player's orientation

    //Input variables for horizontal & vertical movement
    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection; //Direction player will move in
    Rigidbody rb; //Reference to player rigidbody

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Get reference to player rigidbody component and freeze rotation
        //Freeze rotation prevents player from tipping over upon collision
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
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

        //Move player in calculated direction at specified speed
        rb.MovePosition(transform.position + moveDirection.normalized * walkSpeed * Time.fixedDeltaTime);
    }
}
