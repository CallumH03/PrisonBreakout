using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject Crosshair;
    public PlayerMovement playerMovementScript;
    private bool isPaused = false;


    void Start()
    {
        HidePauseMenu(); //Hide pause menu

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //If escape key is pressed
        {
            TogglePause(); //Run togglepause function
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused; //Revereses paused state

        if (isPaused)
        {
            Time.timeScale = 0f; //Pause game timescale
            ShowPauseMenu(); //Show pause menu function
            playerMovementScript.lookSpeed = 0f; //Set lookspeed to 0
            playerMovementScript.walkSpeed = 0f; //Set walkspeed to 0
            Cursor.visible = true; //Show cursor
            Cursor.lockState = CursorLockMode.None;
        }

        else
        {
            Time.timeScale = 1f; //Unpause game
            HidePauseMenu(); //Hide pause menu function
            playerMovementScript.lookSpeed = 2f; //Set lookspeed to 2
            playerMovementScript.walkSpeed = 5f; //Set lookspeed to 5
            Cursor.visible = false; //Hide cursor
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    void ShowPauseMenu() //Enable pause canvas
    {
        pauseMenu.SetActive(true);
    }

    void HidePauseMenu() //Disable pause canvas
    {
        pauseMenu.SetActive(false);
    }

    public void ResumeGame() //Resume game function
    {
        TogglePause();
    }

    public void MainMenu() //Main menu button function
    {
        SceneManager.LoadScene("Menu"); //Load menu scene
        Time.timeScale = 1f; //Ensure timescale is 1
    }
}