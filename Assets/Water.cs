using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Water : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        if(other.transform.gameObject.GetComponent<PlayerController>()) 
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
