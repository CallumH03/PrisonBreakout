using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class EndSceneController : MonoBehaviour
{
    public TMP_Text continueText;
    public Button menuButton;
    public float fadeDuration = 3.0f;

    void Start()
    {
        continueText.color = new Color(continueText.color.r, continueText.color.g, continueText.color.b, 0); //set colour of text to transparent
        menuButton.image.color = new Color(menuButton.image.color.r, menuButton.image.color.g, menuButton.image.color.b, 0); // set colour of button to transparent
        menuButton.GetComponentInChildren<TMP_Text>().color = new Color(0, 0, 0, 0); //set colour of button text to transparent
        menuButton.interactable = false; //Make button not interactable

        StartCoroutine(FadeTextIn(continueText, fadeDuration)); //Start text coroutine
    }

    IEnumerator FadeTextIn(TMP_Text text, float duration) //Text fade in
    {
        yield return new WaitForSeconds(2); //Wait 2 seconds
        while (text.color.a < 1.0f) //Fade text in by increasing alpha value of text
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime / duration));
            yield return null;
        }
        yield return new WaitForSeconds(2); //Wait 2 seconds
        StartCoroutine(FadeButtonIn(menuButton, fadeDuration)); //Start button fade in
    }

    IEnumerator FadeButtonIn(Button button, float duration) //Button fade in
    {
        Image buttonImage = button.image;
        TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();

        buttonText.color = new Color(1f, 1f, 1f, 0f);

        while (buttonImage.color.a < 1.0f) //Fade button in by increasing alpha value of button and text.
        {
            float newAlpha = buttonImage.color.a + (Time.deltaTime / duration);
            buttonImage.color = new Color(buttonImage.color.r, buttonImage.color.g, buttonImage.color.b, newAlpha);
            buttonText.color = new Color(1f, 1f, 1f, newAlpha);
            yield return null;
        }
        button.interactable = true; //Ensure button is interaction
        Cursor.visible = true; //Allow cursor movement
        Cursor.lockState = CursorLockMode.None;
    }

    public void GoToMenu() //Menu button function
    {
        SceneManager.LoadScene("Menu"); 
    }
}