using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Referencies
    private Rigidbody rb;
    private PlayerInputs inputs;

    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float moveLerpSpeed = 10f;
    public float RotationLerpSpeed = 10f;

    [Header("Jump Settings")]
    public float jumpForce = 10f;
    public float gravityStrength = 0.5f;
    public float airControlRatio = 0.5f;


    [Header("Grounded Check")]
    public float checkGroundDistance;

    Vector3 currentOrientation;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputs = GetComponent<PlayerInputs>();
    }

    void FixedUpdate()
    {
        Move(inputs.direction);
        if(inputs.jump && IsGrounded()) Jump(jumpForce);

        if(!IsGrounded()) rb.velocity += Vector3.down * gravityStrength;
    }

    private void Jump(float force)
    {
        rb.velocity = new Vector3(rb.velocity.x, force, rb.velocity.z);
    }

    private void Move(Vector2 direction) // Apply inputs to move the player 
    {
        // Prevents players from going faster in diagonals
        direction = Vector3.ClampMagnitude(direction, 1f);

        // Apply the direction to the velocity of the rigidbody
        Vector3 moveDirection = new Vector3(direction.x, 0f, direction.y);

        if(!IsGrounded()) moveDirection *= airControlRatio;

        rb.velocity = Vector3.Lerp(rb.velocity, moveDirection * moveSpeed + new Vector3(0f, rb.velocity.y, 0f), moveLerpSpeed * Time.deltaTime);

        // Lerp the orientation of the player
        if(moveDirection.magnitude > 0.1f) 
        {
            currentOrientation = moveDirection;
            transform.forward = Vector3.Lerp(transform.forward, currentOrientation, RotationLerpSpeed * Time.deltaTime);
        }
    }    

    private bool IsGrounded() // Simple check if player is on something 
    {
        return Physics.Raycast(transform.position + new Vector3(0f, 0.1f, 0f), -transform.up, checkGroundDistance - 0.1f);
    } 
}