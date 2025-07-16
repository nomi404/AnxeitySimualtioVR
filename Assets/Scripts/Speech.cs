using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speech : MonoBehaviour
{
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Invoke("play",5f);    
    }

    // Update is called once per frame
    void play()
    {
        audioSource.Play(); 
    }
}
