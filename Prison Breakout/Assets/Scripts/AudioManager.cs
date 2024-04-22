using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;


    private void Awake()
    {
        if (Instance == null) //If doesnt exist create audiomanager
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("AudioManager set as persistent instance.");
        }
        else if (Instance != this) //Only allow 1 audio manager
        {
            Debug.Log("Another instance of AudioManager found, destroying new one.");
            Destroy(gameObject);
        }

        ApplyVolumeSettings();
    }

    public void ApplyVolumeSettings() //Apply volume settings audiolistener volume
    {
        float storedVolume = PlayerPrefs.GetFloat("volume", 0.5f);
        AudioListener.volume = storedVolume;
    }

    public void SetVolume(float volume) //Set volume and save to player prefs
    {
        PlayerPrefs.SetFloat("volume", volume);
        PlayerPrefs.Save();
        AudioListener.volume = volume;
    }

}

    