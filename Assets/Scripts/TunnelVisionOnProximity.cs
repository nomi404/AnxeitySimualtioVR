using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.Rendering.Universal;

public class TunnelVisionOnProximity : MonoBehaviour
{
    public Volume volume;  // Reference to the Post-Processing Volume
    public Transform target;  // The object to track proximity (e.g., player)
    public Transform player;  // Reference to the player's transform

    private UnityEngine.Rendering.Universal.Vignette vignette;  // Vignette effect

    public float distanceFactor = 0.1f;  // Adjustable multiplier for distance effect
    public float maxIntensity = 0.6f;  // Max intensity of vignette
    public float maxSmoothness = 0.9f;  // Max smoothness of vignette

    private void Start()
    {
        if (volume.profile.TryGet(out vignette))
        {
            vignette.active = true;  // Ensure vignette is active
        }
    }

    void Update()
    {
        if (vignette != null && player != null && target != null)
        {
            // Calculate the distance between the player and the target
            float distance = Vector3.Distance(player.position, target.position);
            Debug.Log("Distance: " + distance);

            // Ensure the distance doesn't produce extreme values
            if (distance < 0.1f) distance = 0.1f; // Avoid dividing by zero

            // Adjust intensity and smoothness based on distance
            // The closer the player gets to the target, the higher the values
            float intensity = Mathf.Clamp01(1 - (distance * distanceFactor));  // Increases as distance decreases
            float smoothness = Mathf.Clamp01(1 - (distance * distanceFactor));  // Smoothness increases as distance decreases

            // Apply the new values, scaled by maxIntensity and maxSmoothness
            vignette.intensity.value = Mathf.Lerp(0f, maxIntensity, intensity);
            vignette.smoothness.value = Mathf.Lerp(0f, maxSmoothness, smoothness);

            // Debug the values
            Debug.Log("Intensity: " + vignette.intensity.value);
            Debug.Log("Smoothness: " + vignette.smoothness.value);
        }
    }
}
