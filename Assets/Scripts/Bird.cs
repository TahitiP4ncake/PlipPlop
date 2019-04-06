using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }
    
    
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, startPos + Random.insideUnitSphere, Time.fixedDeltaTime*3);
    }
}
