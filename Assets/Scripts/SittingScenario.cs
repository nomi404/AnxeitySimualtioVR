using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class SittingScenario : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource playerAudioSource;
    public AudioClip[] innerThoughtClips;
    public AudioClip[] whisperClips;
    public AudioSource whisperSource;

    [Header("Visuals")]
    public GameObject fadeImage;
    public GameObject coffeeCup;
    public GameObject nightmareObject; // Object with NPCs staring at player

    [Header("Post Processing")]
    public Volume postProcessVolume;
    private Vignette vignette;
    private ChromaticAberration chromatic;
    private LensDistortion lensDistortion;

    public string walkToDoor;
    public GameObject whereToGoEnd;
    public AudioSource leavingAudio;

    void Start()
    {
        // Cache post-processing effects
        postProcessVolume.profile.TryGet(out vignette);
        postProcessVolume.profile.TryGet(out chromatic);
        postProcessVolume.profile.TryGet(out lensDistortion);

        vignette.intensity.Override(0f);
        chromatic.intensity.Override(0f);
        lensDistortion.intensity.Override(0f);

        StartCoroutine(PlaySequence());
    }

    IEnumerator PlaySequence()
    {


        // 1. Play all inner thoughts one by one
        foreach (var thought in innerThoughtClips)
        {
            playerAudioSource.clip = thought;
            playerAudioSource.Play();
            yield return new WaitForSeconds(thought.length + 1f);
        }

        // 2. Blackout
        yield return StartCoroutine(QuickBlackout(fadeImage,1f));

        // 3. Show coffee
        coffeeCup.SetActive(true);
        yield return new WaitForSeconds(0.5f);

        nightmareObject.SetActive(true);
        yield return StartCoroutine(QuickBlackout(fadeImage, 2f));


        // 5. Start visual anxiety effects + whispers
        StartCoroutine(VisualAnxietyEffects());
        yield return StartCoroutine(PlayWhispers());
        nightmareObject.SetActive(false);
        yield return StartCoroutine(QuickBlackout(fadeImage, 2f));

        this.gameObject.SetActive(false);

       SceneManager.LoadScene(walkToDoor);
        whereToGoEnd.SetActive(true);

    }

    IEnumerator QuickBlackout(GameObject gd,float seconds)
    {

        gd.SetActive(true);
        yield return new WaitForSeconds(seconds);
        gd.SetActive(false);

      
    }

    IEnumerator VisualAnxietyEffects()
    {
        float duration = 2f;
        float t = 0;

        // Fade-in effects
        while (t < duration)
        {
            t += Time.deltaTime;
            float intensity = Mathf.Lerp(0f, 0.5f, t / duration);
            vignette.intensity.Override(intensity);
            chromatic.intensity.Override(intensity);
            lensDistortion.intensity.Override(Mathf.Lerp(0f, -0.4f, t / duration));
            yield return null;
        }

        // KEEP EFFECTS ON (do not fade out)
    }

    IEnumerator PlayWhispers()
    {
        foreach (var clip in whisperClips)
        {
            whisperSource.clip = clip;
            whisperSource.Play();
            yield return new WaitForSeconds(clip.length + Random.Range(0.3f, 1.0f));
        }
    }


}
