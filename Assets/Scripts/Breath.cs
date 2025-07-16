using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breath : MonoBehaviour
{
    public AudioSource breathSound;
    public Transform player;
    public Transform stage;

    [Range(1f, 5f)]
    public float maxBreath = 2f;  // Maximum speed of heartbeat, can be adjusted from other scripts
    [Range(0.2f, 1f)]
    public float minBreath = 1f;  // Minimum speed of heartbeat, can be adjusted from other scripts
    public float breathSpeed = 1f; // External modifier for heartbeat speed
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToStage = Vector3.Distance(player.position, stage.position);

        // Adjust heartBeatSpeed based on distance, but also factor in external modifier
        float heartBeatSpeed = Mathf.Lerp(minBreath, maxBreath, 1f - distanceToStage / 15f) * breathSpeed;

        // Apply the modified heartBeatSpeed to the pitch and volume
        breathSound.pitch = heartBeatSpeed;
    }
}
