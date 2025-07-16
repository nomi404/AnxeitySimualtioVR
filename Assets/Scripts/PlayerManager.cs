using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerManager : MonoBehaviour
{
    ContinuousMoveProvider continuousMoveProvider;
    [SerializeField] CameraShake cameraShake;
    [SerializeField] HeartBeat heartBeat;
    [SerializeField] MindBlank mindBlank;
    [SerializeField] GameObject blur;
    [SerializeField] GameObject playerMale;
    [SerializeField] GameObject playerFemale;
    [SerializeField] GameObject blackScreen;
  
    private void Start()
    {
        continuousMoveProvider = gameObject.GetComponent<ContinuousMoveProvider>();
       
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered Trigger with: " + other.name);  // Print the name of the object collided with
        if (other.CompareTag("Stop"))
        {
            Debug.Log("Collision with Stop Trigger detected");
            blackScreen.gameObject.SetActive(true);
            blur.SetActive(false);
            if (PlayerSelection.Instance != null)
            {
                bool isMale = PlayerSelection.Instance.selectedGender == PlayerSelection.Gender.Male;
                if(isMale) { SceneManager.LoadScene("ClassroomSpeechMale"); }
                else { SceneManager.LoadScene("ClassroomSpeechFemale"); }
            }
            else
            {
                Debug.LogWarning("No player selection found, defaulting to male.");
                //playerMale.SetActive(true);
                //playerFemale.SetActive(false);
                SceneManager.LoadScene("SpeechMale");
            }
            cameraShake.shakeAmount = 0.001f;
            heartBeat.heartBeatSpeedModifier = 0.03f;
            mindBlank.enabled = true;   
            Destroy(other.gameObject);
            blur.SetActive(false);
            gameObject.SetActive(false);
        }
    }

    void UpdatePlayerTransform()
    {
        // New position and Euler rotation values
        Vector3 newPosition = new(8.9f, 0.12f, 12.715f);

        // Define the rotation in degrees (Euler angles)
        Vector3 newEulerRotation = new Vector3(0f, 180f, 0f); // Rotate 180 degrees on the Y-axis

        // Update player's position
        transform.position = newPosition;

        // Apply Euler angles to set the rotation
        transform.rotation = Quaternion.Euler(newEulerRotation);

        Debug.Log("Player's position and rotation updated");
    }

}
