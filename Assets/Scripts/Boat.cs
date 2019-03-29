using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : Transport
{

    public bool controlled;


    public Rigidbody rb;
    
    private Vector3 movement;

    private Vector2 input;

    public float speed;
    public float rotationSpeed;

    public float lerpSpeed;
    public float slowDownLerp;

    // Update is called once per frame
    void Update()
    {
        if (controlled)
        {
            CheckInputs();
        }
    }

    void CheckInputs()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        if (Input.GetButton("GamepadJump"))
        {
            print("BOOST");
        }
    }

    private void FixedUpdate()
    {
        if (input.y != 0)
        {
            Move();
        }
        else
        {
            SlowDown();
        }

        if (input.x != 0)
        {
            Turn();
        }

        movement.y = rb.velocity.y;
        
        rb.velocity = movement;
    }


    void Move()
    {
        movement = Vector3.Lerp(movement, transform.forward * input.y * speed, lerpSpeed);
    }

    void SlowDown()
    {
        movement = Vector3.Lerp(movement, Vector3.zero, slowDownLerp);

    }

    void Turn()
    {
        transform.eulerAngles += Vector3.up * input.x * rotationSpeed;
    }

    void Boost()
    {
        
    }
    
    
    
    

    public override void GetIn()
    {
        controlled = true;
    }

    public override void GetOut()
    {
        controlled = false;
    }
}
