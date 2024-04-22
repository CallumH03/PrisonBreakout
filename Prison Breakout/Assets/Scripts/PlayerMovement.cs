using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    //player variables
    public Camera playerCamera;
    public float walkSpeed = 5f;
    public float runSpeed = 5f;
    public float jumpPower = 0f;
    public float gravity = 10f;
    public float lookSpeed = 2f;
    public float lookXLimit = 58f;
    public float defaultHeight = 2f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;

    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private CharacterController characterController;

    private bool canMove = true;

    void Start()
    {
        characterController = GetComponent<CharacterController>(); //Get charactercontroller
        Cursor.lockState = CursorLockMode.Locked; //Lock cursor to centre of screen
        Cursor.visible = false; //Disable cursor
    }

    void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward); //Calculate forward vector
        Vector3 right = transform.TransformDirection(Vector3.right); //Calculate right vector

        bool isRunning = Input.GetKey(KeyCode.LeftShift); //Checks if player is running (currently not in use)
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0; //Calculates movement speed based moving forward/backwards
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0; //Calculates movement speed based moving left/right
        float movementDirectionY = moveDirection.y; //Store current vertical move direction
        moveDirection = (forward * curSpeedX) + (right * curSpeedY); //update movediirection


        if (Input.GetButton("Jump") && canMove && characterController.isGrounded) //if jump is pressed and can move and is grounded
        {
            moveDirection.y = jumpPower; //Apply jump power (currently disabled)
        }
        else
        {
            moveDirection.y = movementDirectionY; //otherwise maintain current vertical movmeent
        }

        if (!characterController.isGrounded) //Apply gravity if not grounded
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.C) && canMove) //Enable crouching
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;
            runSpeed = crouchSpeed;

        }
        else
        {
            characterController.height = defaultHeight; //Reset height and movement when not crouching
            walkSpeed = 5f;
            runSpeed = 5f;
        }

        characterController.Move(moveDirection * Time.deltaTime); //Apply calculated movement vector to character controller

        if (canMove) //Handle player rotation based on mouse movement
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed; //Vertical mouse movement
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit); //limit rotation up or down
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0); //Apply rotation to camera
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0); //Horizontal mouse movement
        }
    }
}