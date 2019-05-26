using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceMainCamera : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        var target = Camera.main.gameObject;
        transform.forward = Camera.main.transform.forward;
    }
}
