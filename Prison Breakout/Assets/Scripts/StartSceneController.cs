using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class StartSceneController : MonoBehaviour
{
    public TMP_Text StartText;
    public float fadeDuration = 5.0f;
    private AudioSource audioSource;

    void Start()
    {
        StartText.color = new Color(StartText.color.r, StartText.color.g, StartText.color.b, 0); //Sets start text to transparent

        audioSource = GetComponent<AudioSource>();

        StartCoroutine(FadeTextIn(StartText, fadeDuration)); //Starts text fade in coroutine

    }

    IEnumerator FadeTextIn(TMP_Text text, float duration)
    {
        Cursor.visible = false; //Disable cursor
        Cursor.lockState = CursorLockMode.Locked;

        yield return new WaitForSeconds(2); //Wait 2 seconds
        while (text.color.a < 1.0f) //Fade in fade by increasing alpha value over time
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / duration));
            yield return null;
        }

        audioSource.Play(); //Play sound
        yield return new WaitForSeconds(2); //Wait 2 seconds
        GoToPrison(); //GotoPrison function
    }

    public void GoToPrison() //Send user to prison scene
    {
        SceneManager.LoadScene("Prison");
    }
}