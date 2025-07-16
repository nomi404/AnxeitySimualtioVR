using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CafeManager : MonoBehaviour
{
    public string secondPlayer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stop"))
        {
            SceneManager.LoadScene(secondPlayer);
            Destroy(this.gameObject);
        }
    }

}
