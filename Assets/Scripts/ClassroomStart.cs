using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ClassroomStart : MonoBehaviour
{
    public Image blackScreen;  // Assign your UI Image in Inspector
    public float fadeDuration = 5f; // Time to fully fade out
    public AudioSource voiceLine; // Assign voice line audio (optional)

    void Start()
    {
        StartCoroutine(FadeOutBlackScreen());
    }

    IEnumerator FadeOutBlackScreen()
    {
        if (voiceLine != null)
        {
            voiceLine.PlayDelayed(2f); 
        }

        float elapsedTime = 0f;
        Color screenColor = blackScreen.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            screenColor.a = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            blackScreen.color = screenColor;
            yield return null;
        }

        blackScreen.gameObject.SetActive(false); // Disable the black screen when done
    }
}
