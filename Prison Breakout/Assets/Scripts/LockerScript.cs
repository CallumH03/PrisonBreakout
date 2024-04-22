using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockerScript : MonoBehaviour
{
    public float interactDistance = 6f;
    private GameObject currentlocker;
    private bool lockerInRange = false;

    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        lockerInRange = Physics.Raycast(ray, out hit, interactDistance) && hit.collider.CompareTag("locker"); //check if looking at object with locker tag

        if (lockerInRange && Input.GetKeyDown(KeyCode.E)) //If in range and pressing "E"
        {
            GameObject lockerParent = hit.collider.gameObject.transform.parent.gameObject; //Get parent object of locker door

            if (currentlocker != lockerParent)
            {
                currentlocker = lockerParent; //ensure correct locker is being looked at
            }

            Animator lockerAnimator = currentlocker.GetComponent<Animator>();
            AudioSource LockerAudioSource = currentlocker.transform.GetChild(0).GetComponent<AudioSource>();

            if (lockerAnimator != null) //If animation is there
            {
                if (!lockerAnimator.IsInTransition(0) && lockerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) //If animation is not playing
                {


                    if (lockerAnimator.GetCurrentAnimatorStateInfo(0).IsName("LockerDoor"))
                    {
                        lockerAnimator.SetTrigger("Close"); //Play close animation
                        LockerAudioSource.Play();
                    }
                    else
                    {
                        lockerAnimator.SetTrigger("Open"); //Play open animation
                        LockerAudioSource.Play();
                    }
                }
            }
        }
    }
}