using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Settings")]
    public float strength = 0.5f;
    public float maxFallSpeed = 10f;
    public float checkGroundDistance;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(!IsGrounded()) 
        {
            Vector3 v = rb.velocity + Vector3.down * strength;
            if(v.y < -maxFallSpeed) v.y = -maxFallSpeed;

            rb.velocity = v;
        }
    }

    private bool IsGrounded() // Simple check if player is on something 
    {
        return Physics.Raycast(transform.position + new Vector3(0f, 0.1f, 0f), -transform.up, checkGroundDistance - 0.1f);
    } 
}
