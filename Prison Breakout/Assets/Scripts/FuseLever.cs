using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseLever : MonoBehaviour
{
    public bool isFusePlaced = false;
    public float interactRange = 4f;
    public Camera playerCamera;
    public bool isAnimating = false;
    public GameObject door;
    public AudioSource leversound;
    public AudioSource dooropensound;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) //Check if user presses E
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactRange)) 
            {
                if (hit.collider.CompareTag("FuseLever")) //Checks if user is looking at object with Tag FuseLever
                {
                    InteractWithLever(hit.collider.gameObject); //Starts function
                }
            }
        }
    }

    void InteractWithLever(GameObject lever) //Interact with lever function
    {
        Animator animator = lever.GetComponent<Animator>();
        Animator doorAnimator = door.GetComponent<Animator>();
        if (animator != null && !isAnimating) //If animator is present and no animating is running
        {
            isAnimating = true; //set isanimating to true only allowing 1 animation during animation time
            if (!isFusePlaced) //If fuse isnt placed
            {
                StartCoroutine(PlayOnThenOffAnimation(animator));
            }
            else //If fuse is placed
            {
                animator.SetTrigger("On"); //Start animation
                leversound.Play();
                doorAnimator.SetTrigger("Open"); //Open door
                dooropensound.Play();

            }
        }
    }

    IEnumerator PlayOnThenOffAnimation(Animator animator) //Play on the off animation
    {
        animator.SetTrigger("On"); //Play on animation
        leversound.Play(); //play sound
        yield return new WaitForSeconds(1);
        animator.SetTrigger("Off"); //Play off animation
        leversound.Play(); //play sound
        yield return new WaitForSeconds(1);
        isAnimating = false;
    }
}