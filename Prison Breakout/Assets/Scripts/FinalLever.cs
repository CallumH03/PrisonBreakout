using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalLever : MonoBehaviour
{
    public Animator leverAnimator;
    public float interactRange = 5f;
    public Camera playerCamera;
    public AudioSource EndaudioSource;
    public AudioSource LeveraudioSource;

    public bool isPulled = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) //Checks if player is pressing "E" 
        {
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, interactRange)) //Check if final lever is in player view using raycast
            {
                if (hit.collider.gameObject == this.gameObject && !isPulled) //Checks if looking at final lever and isnt already pulled
                {
                    PullLever(); //Call pull lever function
                }
            }
        }
    }

    private void PullLever() //Pull lever function
    {
        isPulled = true; //Lever set to pulled
        leverAnimator.SetTrigger("PullLever"); //Start animation
        LeveraudioSource.Play(); //Play lever sound
        StartCoroutine(OpenEndSceneAfterDelay()); //start coroutine
    }

    private IEnumerator OpenEndSceneAfterDelay() //Open end scene after delay
    {
        yield return new WaitForSeconds(1.1f);
        GameObject[] flickerableLights = GameObject.FindGameObjectsWithTag("EndLights"); //Find lights with end lights tag
        foreach (GameObject lightObject in flickerableLights) //For loop going through each light with tag
        {
            Lightflicker flicker = lightObject.GetComponent<Lightflicker>();
            if (flicker != null)
            {
                flicker.enabled = true; //Enabling flicker script for each light
            }
        }

        yield return new WaitForSeconds(1.5f); //Wait 1.5 seconds

        EndaudioSource.Play(); //Play end audio

        foreach (GameObject lightObject in flickerableLights) //For loop going through each light with tag
        {
            Light lightComponent = lightObject.GetComponent<Light>();
            if (lightComponent != null)
            {
                lightComponent.enabled = false; //Turn off lights
            }
        }

        yield return new WaitForSeconds(1f); //Wait 1 second

        SceneManager.LoadScene("EndScene"); //Load EndScene
    }
}