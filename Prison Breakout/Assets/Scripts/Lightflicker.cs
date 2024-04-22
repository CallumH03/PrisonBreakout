using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightflicker : MonoBehaviour
{
    public Light spotlight;
    public float minIntensity = 20f;
    public float maxIntensity = 60f;
    public float minFlickerInterval = 0.1f;
    public float maxFlickerInterval = 0.5f;

    void Start()
    {
        StartCoroutine(Flicker()); // Start the flicker coroutine
    }

    IEnumerator Flicker()
    {
        while (true)
        {
            float flickerIntensity = Random.Range(minIntensity, maxIntensity); // Flicker the light
            spotlight.intensity = flickerIntensity;


            yield return new WaitForSeconds(Random.Range(minFlickerInterval, maxFlickerInterval)); // Wait for a random interval before flickering again
        }
    }
}