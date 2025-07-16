using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    public Image blackScreen;
    public float fadeDuration = 5f;
    private void OnEnable()
    {
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
