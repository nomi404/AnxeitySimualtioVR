using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CafeSecondTrigger : MonoBehaviour
{
    public GameObject secondPlayer;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("s");
        if(collision.gameObject.CompareTag("Player"))
        {
            secondPlayer.SetActive(true);
            Destroy(this.gameObject);
        }
    }
}
