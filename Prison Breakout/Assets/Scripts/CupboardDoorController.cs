using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CupboardDoorController : MonoBehaviour
{
    public float interactionDistance = 8f; //interaction range
    private bool isLeftOpen = false;
    private bool isRightOpen = false;
    public GameObject leftCupboardDoorHinge;
    public GameObject rightCupboardDoorHinge;

    public bool isAnimating = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (IsPlayerLookingAtDoor(leftCupboardDoorHinge) && !isAnimating) //Checks to see if player is looking at left cupboard door and isnt already animating
            {
                isAnimating = true;
                if (isLeftOpen) //If open close cupboard door
                {
                    CloseDoor(leftCupboardDoorHinge);
                    isLeftOpen = false;
                }
                else //If closed open cupboard door
                {
                    OpenDoor(leftCupboardDoorHinge);
                    isLeftOpen = true;
                }
            }
            else if (IsPlayerLookingAtDoor(rightCupboardDoorHinge) && !isAnimating) //Checks to see if player is looking at left cupboard door and isnt already animating
            {
                isAnimating = true;
                if (isRightOpen) //If open close cupboard door
                {
                    CloseDoor(rightCupboardDoorHinge);
                    isRightOpen = false;
                }
                else //If closed open cupboard door
                {
                    OpenDoor(rightCupboardDoorHinge);
                    isRightOpen = true;
                }
            }
        }
    }

    bool IsPlayerLookingAtDoor(GameObject doorHinge) //Checks if player is looking at door using raycast
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance))
        {
            if (hit.collider.gameObject == doorHinge)
            {
                return true;
            }
        }

        return false;
    }

    void OpenDoor(GameObject doorHinge)
    {
        StartCoroutine(ToggleDoorWithDelay(doorHinge, "Open")); //Set cupboard animation to open
    }

    void CloseDoor(GameObject doorHinge)
    {
        StartCoroutine(ToggleDoorWithDelay(doorHinge, "Close")); //Set cupboard animation to close
    }

    IEnumerator ToggleDoorWithDelay(GameObject doorHinge, string trigger) //Play animation and sound for cupboard
    {
        Animator animator = doorHinge.GetComponent<Animator>();
        AudioSource audioSource = doorHinge.GetComponent<AudioSource>();

        animator.SetTrigger(trigger);

        audioSource.Play();

        yield return new WaitForSeconds(0.2f); //time off animation

        isAnimating = false;
    }
}