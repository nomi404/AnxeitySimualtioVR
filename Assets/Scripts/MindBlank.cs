using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MindBlank : MonoBehaviour
{
    public GameObject blackScreen;                        // Reference to the black screen overlay
    public float minTimeBetweenFlickers = 5f;        // Minimum time between flickers
    public float maxTimeBetweenFlickers = 15f;       // Maximum time between flickers
    public float flickerDuration = 1f;             // Duration of each flicker (in seconds)

    private float timeUntilNextFlicker;              // Time left before the next flicker

    void Start()
    {
        // Set the image to be invisible initially
        blackScreen.SetActive(false);

        // Set the initial random flicker time
        SetRandomFlickerTime();
    }

    void Update()
    {
        // Countdown to next flicker
        timeUntilNextFlicker -= Time.deltaTime;

        if (timeUntilNextFlicker <= 0)
        {
            StartCoroutine(Flicker());
            SetRandomFlickerTime();  // Set next flicker time
        }
    }

    IEnumerator Flicker()
    {
        // Enable the black screen for the flicker duration
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(flickerDuration);
        blackScreen.SetActive(false);
    }

    void SetRandomFlickerTime()
    {
        timeUntilNextFlicker = Random.Range(minTimeBetweenFlickers, maxTimeBetweenFlickers);
    }
}
