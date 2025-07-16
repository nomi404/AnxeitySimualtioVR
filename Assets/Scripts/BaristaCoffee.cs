using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaristaCoffee : MonoBehaviour
{
    public AudioSource baristaAudioSource;
    public AudioSource playerAudioSource;
    public AudioSource femaleAudioSource;
    public AudioClip[] baristaLines;
    public AudioClip[] playerMaleLines;
    public AudioClip[] playerFemaleLines;

    public CharacterController characterControllerMale;
    public GameObject maleLoc;
    public GameObject femaleLoc;
    public CharacterController characterControllerFeMale;
    public GameObject walkingBack;
    public GameObject lightBench;
    public GameObject oldText;
    public GameObject newText;
    public GameObject cafesecond;
    public bool male;
    public bool female;

    private int currentLine = 0;
    private bool hasInteracted = false;
    private int totalLines => baristaLines.Length + GetPlayerLines().Length;
    private void Start()
    {
        if (PlayerSelection.Instance != null && PlayerSelection.Instance.selectedGender == PlayerSelection.Gender.Female)
        {
            female = true;
        }

        else male = true; // default
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasInteracted)
        {
            hasInteracted = true;

            if (characterControllerMale != null)
            {
                characterControllerMale.enabled = false;
                maleLoc.SetActive(false);

            }
            if (characterControllerFeMale != null)
            {
                characterControllerFeMale.enabled = false;
                femaleLoc.SetActive(false);
            }

            PlayNextLine();
        }
    }

    void PlayNextLine()
    {
        var playerLines = GetPlayerLines();

        if (currentLine < totalLines)
        {
            if (currentLine % 2 == 0) // Barista's turn
            {
                int index = currentLine / 2;
                baristaAudioSource.clip = baristaLines[index];
                baristaAudioSource.Play();
                Invoke(nameof(PlayNextLine), baristaAudioSource.clip.length + 1f);
            }
            else // Player's turn
            {
                int index = currentLine / 2;

                if(male)
                {
                    playerAudioSource.clip = playerLines[index];
                    playerAudioSource.Play();
                    Invoke(nameof(PlayNextLine), playerAudioSource.clip.length + 1.5f);
                }

                else 
                {
                    femaleAudioSource.clip = playerLines[index];
                    femaleAudioSource.Play();
                    Invoke(nameof(PlayNextLine), femaleAudioSource.clip.length + 1.5f);
                }   

            }

            currentLine++;
        }
        else
        {
            if (characterControllerMale != null) {
                characterControllerMale.enabled = true;
                maleLoc.SetActive(true);

            }
                
            if (characterControllerFeMale != null)
            {
                characterControllerFeMale.enabled = true;
                femaleLoc.SetActive(true);
            }
                

            walkingBack.SetActive(true);
            lightBench.SetActive(true);
            oldText.SetActive(false);
            newText.SetActive(true);
            cafesecond.SetActive(true);
        }
    }

    private AudioClip[] GetPlayerLines()
    {
        if (female)
        {
            return playerFemaleLines;
        }

        return playerMaleLines; // default
    }
}
