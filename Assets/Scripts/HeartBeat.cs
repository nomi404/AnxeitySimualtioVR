using UnityEngine;

public class HeartBeat : MonoBehaviour
{
    public AudioSource heartBeatSound;
    public Transform player;
    public Transform stage;

    [Range(1f, 5f)]
    public float maxHeartBeatSpeed = 2f;  // Maximum speed of heartbeat, can be adjusted from other scripts
    [Range(0.2f, 1f)]
    public float minHeartBeatSpeed = 1f;  // Minimum speed of heartbeat, can be adjusted from other scripts
    public float heartBeatSpeedModifier = 1f; // External modifier for heartbeat speed

    [Range(0.05f, 0.5f)]
    public float minHeartBeatVolume = 0.05f; // Lowered minimum volume
    [Range(0.3f, 0.7f)]
    public float maxHeartBeatVolume = 0.5f; // Lowered max volume

    void Update()
    {
        float distanceToStage = Vector3.Distance(player.position, stage.position);

        // Adjust heartBeatSpeed based on distance, but also factor in external modifier
        float heartBeatSpeed = Mathf.Lerp(minHeartBeatSpeed, maxHeartBeatSpeed, 1f - distanceToStage / 15f) * heartBeatSpeedModifier;

        // Adjust the volume but with reduced loudness
        float heartBeatVolume = Mathf.Lerp(minHeartBeatVolume, maxHeartBeatVolume, 1f - distanceToStage / 15f);

        // Apply changes
        heartBeatSound.pitch = heartBeatSpeed;
        heartBeatSound.volume = heartBeatVolume;
    }
}
