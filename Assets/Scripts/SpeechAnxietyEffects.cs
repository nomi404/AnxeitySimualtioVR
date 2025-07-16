using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class SpeechAnxietyEffects : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource speechAudio;
    public AudioSource heartbeatAudio;

    [Header("Post Processing")]
    public Volume postProcessingVolume;

    [Header("Camera (Optional Shake)")]
    public Transform vrCamera;
    private Vector3 originalCamPos;

    [Header("Scene Settings")]
    public string feedbackSceneName = "FeedbackScene";

    private DepthOfField depthOfField;
    private Vignette vignette;
    private ChromaticAberration chromaticAberration;
    private LensDistortion lensDistortion;
    private ColorAdjustments colorAdjustments;

    void OnEnable()
    {
        postProcessingVolume.profile.TryGet(out depthOfField);
        postProcessingVolume.profile.TryGet(out vignette);
        postProcessingVolume.profile.TryGet(out chromaticAberration);
        postProcessingVolume.profile.TryGet(out lensDistortion);
        postProcessingVolume.profile.TryGet(out colorAdjustments);

        // Reset all effects
        if (depthOfField != null) depthOfField.focalLength.value = 0f;
        if (vignette != null) vignette.intensity.value = 0f;
        if (chromaticAberration != null) chromaticAberration.intensity.value = 0f;
        if (lensDistortion != null) lensDistortion.intensity.value = 0f;
        if (colorAdjustments != null) colorAdjustments.saturation.value = 0f;

        if (vrCamera != null) originalCamPos = vrCamera.localPosition;

        StartCoroutine(PlaySpeechAndStartEffects());
    }

    IEnumerator PlaySpeechAndStartEffects()
    {
        yield return new WaitForSeconds(5f); // Delay before starting

        speechAudio?.Play();
        heartbeatAudio?.Play();

        float speechDuration = speechAudio.clip.length;

        StartCoroutine(ProgressiveSymptoms(speechDuration));
        StartCoroutine(CameraShakeRoutine(speechDuration));

        yield return new WaitUntil(() => !speechAudio.isPlaying);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(feedbackSceneName);
    }

    IEnumerator ProgressiveSymptoms(float totalDuration)
    {
        float phase1 = 8f;
        float phase2 = 14f;
        float phase3 = Mathf.Max(totalDuration - phase1 - phase2, 2f);

        // ---------- Phase 1: Subtle Discomfort ----------
        for (float t = 0f; t < phase1; t += Time.deltaTime)
        {
            float p = t / phase1;
            if (vignette != null)
            {
                vignette.smoothness.Override(1f);
                vignette.intensity.value = Mathf.Lerp(0f, 0.6f, p);
            }
            if (depthOfField != null)
                depthOfField.focalLength.value = Mathf.Lerp(0f, 25f, p);
            if (chromaticAberration != null)
                chromaticAberration.intensity.value = Mathf.Lerp(0f, 0.4f, p);
            if (lensDistortion != null)
                lensDistortion.intensity.value = Mathf.Lerp(0f, -0.25f, p);
            if (colorAdjustments != null)
                colorAdjustments.saturation.value = Mathf.Lerp(0f, -40f, p);

            yield return null;
        }

        // ---------- Phase 2: Panic Peak ----------
        for (float t = 0f; t < phase2; t += Time.deltaTime)
        {
            float p = t / phase2;
            if (vignette != null)
                vignette.intensity.value = Mathf.Lerp(0.6f, 0.9f, p);
            if (depthOfField != null)
                depthOfField.focalLength.value = Mathf.Lerp(25f, 60f, p);
            if (chromaticAberration != null)
                chromaticAberration.intensity.value = Mathf.Lerp(0.4f, 0.8f, p);
            if (lensDistortion != null)
                lensDistortion.intensity.value = Mathf.Lerp(-0.25f, -0.5f, p);
            if (colorAdjustments != null)
                colorAdjustments.saturation.value = Mathf.Lerp(-40f, -100f, p);

            // Optional: flicker brightness
            if (colorAdjustments != null && t % 1f < 0.1f)
                colorAdjustments.postExposure.value = Random.Range(-1f, -0.3f);

            yield return null;
        }

        // ---------- Phase 3: Emotional Shutdown ----------
        for (float t = 0f; t < phase3; t += Time.deltaTime)
        {
            float p = t / Mathf.Max(phase3, 0.01f);
            if (vignette != null)
                vignette.intensity.value = Mathf.Lerp(0.9f, 1f, p);
            if (depthOfField != null)
                depthOfField.focalLength.value = Mathf.Lerp(60f, 90f, p);
            if (chromaticAberration != null)
                chromaticAberration.intensity.value = Mathf.Lerp(0.8f, 0.3f, p);
            if (lensDistortion != null)
                lensDistortion.intensity.value = Mathf.Lerp(-0.5f, -0.7f, p);
            if (colorAdjustments != null)
            {
                colorAdjustments.saturation.value = Mathf.Lerp(-100f, -120f, p);
                colorAdjustments.postExposure.value = Mathf.Lerp(-0.5f, -1.5f, p);
            }

            yield return null;
        }
    }


    IEnumerator CameraShakeRoutine(float duration)
    {
        if (vrCamera == null) yield break;

        float elapsed = 0f;
        float frequency = 0.05f;
        float intensity = 0.005f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            vrCamera.localPosition = originalCamPos + Random.insideUnitSphere * intensity;
            yield return new WaitForSeconds(frequency);
        }

        vrCamera.localPosition = originalCamPos;
    }
}
