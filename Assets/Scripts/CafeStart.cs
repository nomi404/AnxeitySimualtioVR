using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CafeStart : MonoBehaviour
{
    public Image blackScreen;  // Assign your UI Image in Inspector
    public float fadeDuration = 5f; // Time to fully fade out
    public GameObject female;
    public GameObject male;


    void Start()
    {
        if (PlayerSelection.Instance != null && PlayerSelection.Instance.selectedGender == PlayerSelection.Gender.Female)
            female.SetActive(true);
        else male.SetActive(true);

        StartCoroutine(FadeOutBlackScreen());
    }

    IEnumerator FadeOutBlackScreen()
    {

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
