using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Referencies

    Rigidbody rb;
    PlayerInputs inputs;
    CameraRotation cam;
    BodyAnimations legs;
    Absorber absorber;
    PlayerChatter chatter;
    Pilot pilot;

    bool isFrozen = true;


    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float moveLerpSpeed = 10f;
    public float RotationLerpSpeed = 10f;

    [Header("Jump Settings")]
    public float jumpForce = 10f;
    public float airControlRatio = 0.5f;

    [Header("Grounded Check")]
    public float checkGroundDistance;
    Vector3 currentOrientation;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        inputs = GetComponent<PlayerInputs>();
        legs = GetComponentInChildren<BodyAnimations>();
        absorber = GetComponent<Absorber>();
        chatter = GetComponent<PlayerChatter>();
        pilot = GetComponent<Pilot>();
    }

    void Start()
    {
        // Spawn Camera
        //cam = Instantiate(Library.instance.playerCameraPrefab).GetComponent<CameraRotation>();
        //cam.playerTransform = absorber.head;
    }

    void FixedUpdate()
    {
        if(!isFrozen) Move(inputs.direction);

        legs.velocity = rb.velocity;
    }

    void Update()
    {
        // Check if player is grounded
        bool isGrounded = IsGrounded();

        // Check Jump Inputs

        if(inputs.jump && isGrounded) Jump(jumpForce);

        // Check Possess Inputs
        if (inputs.jump && isGrounded && !isFrozen) Jump(jumpForce);
        // Check Possess Inputs
        if (inputs.possess) {
            pilot.TryTakeControl();
            absorber.TryAbsorb();
        }
        else if (inputs.unpossess) {
            pilot.TryLooseControl();
            absorber.ReleaseLast();
        }

        if (inputs.talk) chatter.TryTalk();

        // Parse values to legs
        //legs.SetGrounded(isGrounded);
    }

    private void Jump(float force) // Make the player jump 
    {
        rb.velocity = new Vector3(rb.velocity.x, force, rb.velocity.z);
        //legs.Jump();
    }

    private void Move(Vector2 direction) // Apply inputs to move the player 
    {
        // Prevents players from going faster in diagonals
        direction = Vector3.ClampMagnitude(direction, 1f);

        // Lerp the rb velocity depending on the camera rotation and inputs
        rb.velocity = Vector3.Lerp(
            rb.velocity,
            (cam.transform.right * direction.x + cam.transform.forward * direction.y) * moveSpeed + new Vector3(0f, rb.velocity.y, 0f),
            moveLerpSpeed * Time.deltaTime
        );

        // Lerp the orientation of the player
        if(direction.magnitude > 0.1f) 
        {
            currentOrientation = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
            transform.forward = Vector3.Lerp(transform.forward, currentOrientation, RotationLerpSpeed * Time.deltaTime);
        }

        // Parse speed to legs
        //legs.SetSpeed(direction.magnitude);
    }    

    private bool IsGrounded() // Simple check if player is on something 
    {
        return Physics.Raycast(transform.position + new Vector3(0f, 0.1f, 0f), -transform.up, checkGroundDistance - 0.1f);
    } 

    public void ActivateMovement(){isFrozen = false;}
    public void StopMovement(){ isFrozen = true;}

    public void ActivePhysics()
    {
        rb.useGravity = true;
        rb.isKinematic = false;
    }
    public void StopPhysics()
    {
        rb.useGravity = false;
        rb.isKinematic = true;
    }
}