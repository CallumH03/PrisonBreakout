using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject controlsPanel;
    public Slider volumeSlider;
    public CanvasGroup mainMenuGroup;
    public AudioSource ButtonSound;

    void Start()
    {
        settingsPanel.SetActive(false); //Disable settings panel
        controlsPanel.SetActive(false); //Disable control panel
        volumeSlider.value = PlayerPrefs.GetFloat("volume", 0.5f); //set volume to playerprefs
        OnVolumeChange();
    }


    public void PlayGame() //Load startscene
    {
        SceneManager.LoadScene("StartScene");
    }

    public void QuitGame() //Quit game
    {
        Application.Quit();

#if UNITY_EDITOR //Quit game if in unity editor
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void OpenSettings() //Enable settings panel
    {
        settingsPanel.SetActive(true);
        mainMenuGroup.interactable = false;
        mainMenuGroup.alpha = 0f;
    }

    public void OpenControls() //Enable controls panel
    {
        controlsPanel.SetActive(true);
        mainMenuGroup.interactable = false;
        mainMenuGroup.alpha = 0f;
    }

    public void CloseSettings() //Close settings panel and set volume pref
    {
        settingsPanel.SetActive(false);
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
        mainMenuGroup.interactable = true;
        mainMenuGroup.alpha = 1.0f;
    }

    public void CloseControls() //Close control panel
    {
        controlsPanel.SetActive(false);
        mainMenuGroup.interactable = true;
        mainMenuGroup.alpha = 1.0f;
    }

    public void OnVolumeChange() //Set new volume on change
    {
        AudioManager.Instance.SetVolume(volumeSlider.value);
    }

    public void PlayButtonSound() //Play button sound
    {
        if (ButtonSound != null && ButtonSound.clip != null)
        {
            ButtonSound.Play();
        }
    }
}