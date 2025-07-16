using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CafeEnd : MonoBehaviour
{
    public GameObject blackScreen;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if(other.gameObject.CompareTag("Stop"))
        {
            blackScreen.SetActive(true);
            SceneManager.LoadScene("feedback");
        }
    }
}
