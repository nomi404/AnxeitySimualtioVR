using System.Collections;
using UnityEngine;

public class InternalMonologue : MonoBehaviour
{
    public AudioSource playerThoughtsAudio;
    public AudioClip[] thoughtLines;
    public float delayBetweenThoughts = 1.2f;

    private void OnEnable()
    {
        StartCoroutine(PlayThoughtSequence());
    }

    IEnumerator PlayThoughtSequence()
    {

        for (int i = 0; i < thoughtLines.Length; i++)
        {
            playerThoughtsAudio.clip = thoughtLines[i];
            playerThoughtsAudio.Play();
            yield return new WaitForSeconds(thoughtLines[i].length + delayBetweenThoughts);
        }


    }
}
