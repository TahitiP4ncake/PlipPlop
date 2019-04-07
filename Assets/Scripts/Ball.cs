using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    public Rigidbody rb;

    public float BounceSpeed;

    public float gravity;


    private Vector3 startPosition;
    
    private void Start()
    {
        startPosition = transform.position;
    }

    private void OnCollisionEnter(Collision other)
    {
        
        Manager.SINGLETON.PlaySound("bounce", .4f);

        if (other.collider.CompareTag("Player"))
        {
            Bounce();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Basket"))
        {
            Manager.SINGLETON.PlaySound("victory", .15f);
        }
    }


    private void FixedUpdate()
    {
        rb.velocity += Vector3.down * gravity;


        if (transform.position.y < -7)
        {
            transform.position = startPosition;
            
            rb.velocity = new Vector3(0,BounceSpeed, 0);

            rb.angularVelocity = Vector3.zero;
            
            Manager.SINGLETON.PlaySound("bounce", .4f);
        }
    }

    void Bounce()
    {
        rb.velocity = new Vector3(rb.velocity.x/2,BounceSpeed , rb.velocity.z/2);
        
        
    }
}
