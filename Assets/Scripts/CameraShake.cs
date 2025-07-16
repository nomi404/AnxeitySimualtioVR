using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Transform cameraTransform;
    public Transform player;
    public Transform stage;
    public float shakeAmount = 0.05f;

    void Update()
    {
        float distanceToStage = Vector3.Distance(player.position, stage.position);
        float shakeIntensity = Mathf.Lerp(0f, shakeAmount, 1f - distanceToStage / 10f);

        Vector3 randomOffset = Random.insideUnitSphere * shakeIntensity;
        randomOffset.z = 0;  // Keep Z-axis steady to avoid motion sickness in VR.
        cameraTransform.localPosition += randomOffset;
    }
}
