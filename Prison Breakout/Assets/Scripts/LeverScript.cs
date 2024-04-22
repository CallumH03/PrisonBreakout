using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour
{
    public float interactionDistance = 10f;
    private bool Lever1On = false;
    private bool Lever2On = false;
    private bool Lever3On = false;
    private bool Lever4On = false;
    private bool Lever5On = false;
    public GameObject Lever1;
    public GameObject Lever2;
    public GameObject Lever3;
    public GameObject Lever4;
    public GameObject Lever5;
    //sets all levers to off

    public GameObject hatch;

    public GameObject DrainTeleport;

    public bool HatchOpen = false;

    public bool isAnimating = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !HatchOpen) //Checks if user has pressed E and hatch isnt open
        {
            if (IsPlayerLookingAtLever(Lever1) && !isAnimating) //Lever 1 if statement
            {
                isAnimating = true; //Sets bool to true
                if (Lever1On) //If lever is on
                {
                    LeverOff(Lever1); //Run off function
                    Lever1On = false; //Set bool to false (lever off)
                }
                else //If lever is off
                {
                    LeverOn(Lever1); //Run on function
                    Lever1On = true; //Set bool to false (lever on)
                }
            }
            else if (IsPlayerLookingAtLever(Lever2) && !isAnimating) //Lever 2 if statement
            {
                isAnimating = true;
                if (Lever2On)
                {
                    LeverOff(Lever2);
                    Lever2On = false;
                }
                else
                {
                    LeverOn(Lever2);
                    Lever2On = true;
                }
            }
            else if (IsPlayerLookingAtLever(Lever3) && !isAnimating) //Lever 3 if statement
            {
                isAnimating = true;
                if (Lever3On)
                {
                    LeverOff(Lever3);
                    Lever3On = false;
                }
                else
                {
                    LeverOn(Lever3);
                    Lever3On = true;
                }
            }
            else if (IsPlayerLookingAtLever(Lever4) && !isAnimating) //Lever 4 if statement
            {
                isAnimating = true;
                if (Lever4On)
                {
                    LeverOff(Lever4);
                    Lever4On = false;
                }
                else
                {
                    LeverOn(Lever4);
                    Lever4On = true;
                }
            }
            else if (IsPlayerLookingAtLever(Lever5) && !isAnimating) //Lever 5 if statement
            {
                isAnimating = true;
                if (Lever5On)
                {
                    LeverOff(Lever5);
                    Lever5On = false;
                }
                else
                {
                    LeverOn(Lever5);
                    Lever5On = true;
                }
            }

            if (Lever1On && !Lever2On && Lever3On && Lever4On && !Lever5On) //If correct lever combination
            {
                DrainTeleport.SetActive(true); //Enable drain teleporter
                OpenHatch(); //Play openhatch function
                HatchOpen = true; //Hatchopen set to true
            }
        }
    }

    bool IsPlayerLookingAtLever(GameObject lever) //Check is player is looking at lever
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance)) //If looking at lever and in range
        {
            if (hit.collider.gameObject == lever)
            {
                return true; //sets gameobject lever to that lever
            }
        }

        return false;
    }

    void OpenHatch() //Open hatch function
    {
        hatch.GetComponent<Animator>().SetTrigger("Open"); //Play open animation
        AudioSource DrainOpenSound = hatch.GetComponent<AudioSource>();
        DrainOpenSound.Play(); //Play sound
    }

    void LeverOn(GameObject lever)
    {
        StartCoroutine(ToggleLeverWithDelay(lever, "On")); //Play lever animation on
    }

    void LeverOff(GameObject lever)
    {
        StartCoroutine(ToggleLeverWithDelay(lever, "Off")); //Play lever animation off
    }

    IEnumerator ToggleLeverWithDelay(GameObject lever, string trigger)
    {
        Animator animator = lever.GetComponent<Animator>();
        AudioSource audioSource = lever.GetComponent<AudioSource>();
        animator.SetTrigger(trigger); //Play animation with trigger variable
        audioSource.Play(); //play sound

        yield return new WaitForSeconds(0.45f); //Wait till animation is finished

        isAnimating = false; //Allow lever to be used again
    }
}