using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerScript : MonoBehaviour
{
    public float interactDistance = 6f; //Interaction range
    private GameObject currentDrawer;
    private bool drawerInRange = false;

    void Update()
    {
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        drawerInRange = Physics.Raycast(ray, out hit, interactDistance) && hit.collider.CompareTag("drawer"); //Sets drawerinrange to current drawer player is looking at

        if (drawerInRange && Input.GetKeyDown(KeyCode.E))
        {
            GameObject drawerParent = hit.collider.gameObject.transform.parent.gameObject; //Gets parents to drawer (which holds animation)

            if (currentDrawer != drawerParent) //Sets drawer parent if not already
            {
                currentDrawer = drawerParent;
            }

            Animator drawerAnimator = currentDrawer.GetComponent<Animator>(); //gets drawer animation
            AudioSource drawerAudioSource = currentDrawer.transform.GetChild(0).GetComponent<AudioSource>(); //gets drawer sound

            if (drawerAnimator != null) //If drawer animator exists
            {
                if (!drawerAnimator.IsInTransition(0) && drawerAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f) //if not already playing
                {
                    if (drawerAnimator.GetCurrentAnimatorStateInfo(0).IsName("DrawerAnimation"))
                    {
                        drawerAnimator.SetTrigger("Close"); //Plays close animation
                        drawerAudioSource.Play();
                    }
                    else
                    {
                        drawerAnimator.SetTrigger("Open"); //Plays open animation
                        drawerAudioSource.Play();
                    }
                }
            }
        }
    }
}