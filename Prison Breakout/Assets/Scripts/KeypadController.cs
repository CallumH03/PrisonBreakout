using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeypadController : MonoBehaviour
{
    public TMP_Text codeDisplay;
    public GameObject redOverlay;
    public GameObject greenOverlay;
    private string enteredCode = "";
    private string correctCode = "923";
    public GameObject door;

    public GameObject keycodecanvas;
    public PlayerMovement playerMovementScript;
    private bool isPaused = false;
    public Camera playerCamera;
    public float interactRange = 3f;

    public AudioSource doorsound;

    void Start()
    {
        HideKeycode(); //Hide keycode UI

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) //Checks if user is pressing E
        {
            if (!isPaused) //Checks if game is not paused
            {
                RaycastHit hit;
                if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactRange))
                {
                    if (hit.collider.CompareTag("Keypad")) //checks if looking at Keypad
                    {
                        ToggleKeypad(); //Starts function
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape)) //Checks if user presses Escape key
        {
            HideKeycodeEscape(); //Hides keycode (Escape function)
            isPaused = false; //Sets to game not paused
        }
    }



    public void OnButtonPressed(string number) //On each button press
    {
        if (enteredCode.Length < 3)
        {
            enteredCode += number; //Add number to entered code
            codeDisplay.text = enteredCode; //Show entered code on keypad
        }

        if (enteredCode.Length == 3) //Once 3 digits entered
        {
            CheckCode(); //Check code function
        }
    }

    private void CheckCode() //Check code function
    {
        if (enteredCode == correctCode) //If entered code is correct
        {
            greenOverlay.SetActive(true); //Set keypad to green (indicating success)
            Debug.Log("Code Correct. Door unlocking...");
            OpenDoor(); //Play door animation
        }
        else //If incorrect code
        {
            Debug.Log("Incorrect Code. Try again.");
            StartCoroutine(ResetCode()); //Reset code
        }
    }


    void ToggleKeypad() //Toggle keypad function
    {
        isPaused = !isPaused;  //Reverses paused state

        if (isPaused) //If game is paused
        {
            ShowKeycode(); //show keypad
            playerMovementScript.lookSpeed = 0f; //set lookspeed to 0
            playerMovementScript.walkSpeed = 0f; //set walkspeed to 0
            Cursor.visible = true; //make cursor visible
            Cursor.lockState = CursorLockMode.None;
        }

        else //If game isnt paused
        {
            HideKeycode(); //Hide keycode
            playerMovementScript.lookSpeed = 2f; //set lookspeed to 2f
            playerMovementScript.walkSpeed = 5f; //set walkspeed to 5f
            Cursor.visible = false; //make cursor invisible
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void OpenDoor() //Open door function
    {
        Animator doorAnimator = door.GetComponent<Animator>();
        if (doorAnimator != null) //If animator exists
        {

            doorAnimator.SetTrigger("OpenDoor"); //Play door animation
            doorsound.Play(); //Play sound


        }
        else //if animator doesnt exist
        {
            Debug.LogError("Animator component not found on the door object.");
        }
    }

    void ShowKeycode() //Show keycode function
    {
        keycodecanvas.SetActive(true); //Make UI canvas visible
    }

    public void HideKeycode() //Hide keycode function
    {
        keycodecanvas.SetActive(false); //Disables keycode canvas
        isPaused = false; //set to not paused
        playerMovementScript.lookSpeed = 2f; //set lookspeed to 2f
        playerMovementScript.walkSpeed = 5f; //set walkspeed to 5f
        Cursor.visible = false; //make cursor invisible
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void HideKeycodeEscape() //Hide keycode upon "Escape" key function
    {
        keycodecanvas.SetActive(false); //Hide canvas UI

    }

    private IEnumerator ResetCode() //Reset code function
    {
        redOverlay.SetActive(true); //Enable red overlay on canvas
        yield return new WaitForSeconds(1); //Wait 1 second
        enteredCode = ""; //Reset entered code
        codeDisplay.text = enteredCode; //Apply reset to UI display
        redOverlay.SetActive(false); //Display red overlay on canvas
    }
}