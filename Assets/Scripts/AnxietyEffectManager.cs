using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;  // Correct namespace for URP

public class AnxietyEffectManager : MonoBehaviour
{
    public Transform player;           // Reference to the player
    public Transform stage;            // Reference to the stage
    public Volume volume;              // Reference to the URP Volume
    public float maxAnxietyDistance = 10f;  // Distance where anxiety peaks
    private ColorAdjustments colorAdjustments; // URP Color Adjustments component

    void Start()
    {
        // Try to get the Color Adjustments from the Volume Profile (URP)
        if (volume.profile.TryGet(out colorAdjustments))
        {
            colorAdjustments.active = true;  // Enable Color Adjustments if found
        }
        else
        {
            Debug.LogError("Color Adjustments not found in Volume Profile!");
        }
    }

    void Update()
    {
        // Calculate the distance between player and stage
        float distance = Vector3.Distance(player.position, stage.position);

        // Normalize the distance value (0 when close, 1 when far)
        float normalizedDistance = Mathf.Clamp01(distance / maxAnxietyDistance);

        // Adjust the color filter based on distance (closer = more red tint)
        Color tintColor = Color.Lerp(Color.white, Color.red, 1f - normalizedDistance);
        colorAdjustments.colorFilter.value = tintColor;  // Apply the red tint as anxiety peaks
    }
}
