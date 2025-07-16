using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BlurEffectManager : MonoBehaviour
{
    public Transform stage;              // Reference to the stage
    public Transform player;             // Reference to the player
    public Volume volume;                // Reference to the URP Volume

    [Header("Distance Settings")]
    public float maxBlurDistance = 10f;

    [Header("Blur Settings")]
    public float minAperture = 0.1f;
    public float maxAperture = 32f;
    public float minBlurSize = 0.1f;
    public float maxBlurSize = 5f;

    [Header("Tunnel Vision & Effects")]
    public float maxVignetteIntensity = 0.6f;
    public float maxAberrationIntensity = 0.5f;

    [Header("Color Anxiety Settings")]
    public Color maxAnxietyColor = new Color(0.9f, 0.2f, 0.2f); // deep soft red
    public float maxExposure = -0.3f;
    public float maxContrast = 30f;

    private DepthOfField depthOfField;
    private Vignette vignette;
    private ChromaticAberration chromaticAberration;
    private ColorAdjustments colorAdjustments;

    void Start()
    {
        if (!volume.profile.TryGet(out depthOfField))
            Debug.LogError("Depth of Field not found in Volume Profile!");
        if (!volume.profile.TryGet(out vignette))
            Debug.LogWarning("Vignette not found in Volume Profile!");
        if (!volume.profile.TryGet(out chromaticAberration))
            Debug.LogWarning("Chromatic Aberration not found in Volume Profile!");
        if (!volume.profile.TryGet(out colorAdjustments))
            Debug.LogWarning("Color Adjustments not found in Volume Profile!");
    }

    void Update()
    {
        float distance = Vector3.Distance(player.position, stage.position);
        float normalizedDistance = Mathf.Clamp01(distance / maxBlurDistance);
        float closeness = 1f - normalizedDistance; // 1 = very close

        // ---------- Depth of Field ----------
        if (depthOfField != null)
        {
            depthOfField.aperture.value = Mathf.Lerp(maxAperture, minAperture, closeness);
            float blurSize = Mathf.Lerp(maxBlurSize, minBlurSize, closeness);
            depthOfField.gaussianStart.value = blurSize;
            depthOfField.gaussianEnd.value = blurSize + 2f;
        }

        // ---------- Vignette ----------
        if (vignette != null)
        {
            vignette.intensity.Override(Mathf.Lerp(0f, maxVignetteIntensity, closeness));
            vignette.smoothness.Override(0.9f);
        }

        // ---------- Chromatic Aberration ----------
        if (chromaticAberration != null)
        {
            chromaticAberration.intensity.Override(Mathf.Lerp(0f, maxAberrationIntensity, closeness));
        }

        // ---------- Anxiety Red Tint ----------
        if (colorAdjustments != null)
        {
            colorAdjustments.colorFilter.Override(Color.Lerp(Color.white, maxAnxietyColor, closeness));
            colorAdjustments.postExposure.Override(Mathf.Lerp(0f, maxExposure, closeness));
            colorAdjustments.contrast.Override(Mathf.Lerp(0f, maxContrast, closeness));
        }
    }
}
