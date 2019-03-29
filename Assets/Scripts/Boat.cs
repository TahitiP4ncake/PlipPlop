using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : Transport
{

    public bool controlled;


    public Rigidbody rb;
    
    private Vector3 movement;
    private Vector3 direction;

    private Vector2 input;

    public float speed;
    public float rotationSpeed;

    public float lerpSpeed;
    public float slowDownLerp;

    public float sideTilt;
    public float frontTilt;
    public float maxTiltAngle;

    public Transform camTransform;

    public GameObject fx;

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
        if (input.y != 0 || input.x !=0)
        {
            Move();
            Turn();

            if (fx.activeSelf == false)
            {
                fx.SetActive(true);
            }

        }
        else
        {
            
            if (fx.activeSelf == true)
            {
                fx.SetActive(false);
            }
            
            
            float _angle = (transform.localEulerAngles.z > 180) ? transform.localEulerAngles.z - 360 : transform.localEulerAngles.z;
        
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,transform.localEulerAngles.y,Mathf.Clamp(Mathf.Lerp(_angle, 0*sideTilt,.5f),  -maxTiltAngle, maxTiltAngle));
            
            
            SlowDown();
        }
    
        //        transform.localEulerAngles = new Vector3(Mathf.Lerp(transform.localEulerAngles.x, Mathf.Clamp(movement.magnitude*frontTilt, -maxTiltAngle, maxTiltAngle), .5f),transform.localEulerAngles.y,transform.localEulerAngles.z);

        transform.localEulerAngles = new Vector3(Mathf.Clamp(movement.magnitude* - frontTilt, -maxTiltAngle, maxTiltAngle),transform.localEulerAngles.y,transform.localEulerAngles.z);

        
        //movement = transform.forward * actualSpeed;


        movement.y = rb.velocity.y;
        
        rb.velocity = movement;
    }


    void Move()
    {
        //actualSpeed = Mathf.Clamp(actualSpeed + input.y * speed, minSpeed, maxSpeed);
        //movement = Vector3.Lerp(movement, Vector3.ClampMagnitude(camTransform.right*input.x + camTransform.forward* input.y, 1f) * speed, lerpSpeed);
        movement = Vector3.Lerp(movement, transform.forward * speed, lerpSpeed);
    }

    void SlowDown()
    {
        movement = Vector3.Lerp(movement, Vector3.zero, slowDownLerp);
    }

    void Turn()
    {
        direction = new Vector3(0, Mathf.Atan2(input.y, -input.x) * 180 / Mathf.PI-90 + camTransform.eulerAngles.y, 0);

        float _rotation = Mathf.DeltaAngle (transform.eulerAngles.y, direction.y);
        
        
        float step = rotationSpeed * Mathf.Abs (_rotation)/180;
        
        _rotation = (_rotation > 180) ? _rotation - 360 : _rotation;

        float _angle = (transform.localEulerAngles.z > 180) ? transform.localEulerAngles.z - 360 : transform.localEulerAngles.z;
        
        transform.localEulerAngles = new Vector3(0,transform.localEulerAngles.y,Mathf.Clamp(Mathf.Lerp(_angle, _rotation*sideTilt,.2f),  -maxTiltAngle, maxTiltAngle));

        
		
        Quaternion turnRotation = Quaternion.Euler(0f, direction.y, 0f);
				
        transform.transform.localRotation = Quaternion.RotateTowards(transform.localRotation, turnRotation, step );    }

    void Boost()
    {
        
    }
    
    
    
    

    public override void GetIn()
    {
        controlled = true;

        rb.isKinematic = false;
        print("GETIN");
    }

    public override void GetOut()
    {
        controlled = false;

        rb.isKinematic = true;
        
        print("GETOUT");

    }
}
