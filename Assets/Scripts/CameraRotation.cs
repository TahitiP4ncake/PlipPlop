using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Transform playerTransform;

    public float lerpSpeed;
    public float xSpeed;
    public float ySpeed;
    
    public Transform camXRotation;
    public Vector2 xLimits;
    
    //Internal Data

    private Vector2 input;
    
    void Update()
    {
        CheckInputs();
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, playerTransform.position, lerpSpeed);
        
        
        transform.localEulerAngles += new Vector3(0,input.x * xSpeed ,0);
        camXRotation.localEulerAngles = new Vector3( Mathf.Clamp( input.y *-ySpeed  + camXRotation.localEulerAngles.x, xLimits.x, xLimits.y),0,0);

    }

    void CheckInputs()
    {
        input.x = Input.GetAxis("Mouse X") + Input.GetAxis("RightX");
        input.y = Input.GetAxis("Mouse Y") - Input.GetAxis("RightY");
    }
}
