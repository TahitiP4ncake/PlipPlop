﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Transform camTransform;
    
    public Rigidbody rb;

    [Space] [Header("Movement Values")] 
    public float speed;
    public float rotationSpeed;

    public float jumpForce;

    [Space] 
    
    public float moveLerpSpeed;
    public float stopLerpSpeed;

    [Space] 
    
    public float checkGroundDistance;
    public float gravityStrength;


    [Space] 
    
    public Transform visualsObject;
    public Transform visualsParent;
    public float visualsLerpSpeed;


    [Space] 
    
    public float YKILL;

    [Space] 
    
    public List<GameObject> objects = new List<GameObject>();

    public  List<Vector3> offsets = new List<Vector3>();

    public Transform lastObject;

    public Transform baseObject;
    
    
    [Space] 
    public Transform footTransform;
    public Transform hipsTransform;
    public Transform headTransform;
    
    //Internal Data

    private Vector2 input;
    
    private Vector3 movement;
    private Vector3 direction;

    private float gravityValue;

    public bool grounded;

    public bool lerpVisuals;

    void Start()
    {
        if(lerpVisuals)
        visualsObject.parent = null;

        lastObject = visualsObject.GetChild(0);
    }
    
    void Update()
    {
        CheckInputs();
    }

    void FixedUpdate()
    {
        if (input.x != 0 || input.y != 0)
        {
            Move();
            Turn();

        }
        else
        {
            Stop();
        }

        if (!grounded)
        {
            rb.velocity += Vector3.down * gravityStrength;
        }
        
        movement.y = rb.velocity.y;

        rb.velocity = movement;


        if (lerpVisuals)
            LerpVisuals();



        if (transform.position.y < YKILL)
        {
            transform.position = Vector3.zero;
            rb.velocity = Vector3.zero;
        }
    }

    void LerpVisuals()
    {
        visualsObject.transform.eulerAngles = visualsParent.transform.eulerAngles;

        visualsObject.transform.position = Vector3.Lerp(visualsObject.position, visualsParent.position,visualsLerpSpeed);
    }

    void CheckInputs()
    {
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("GamepadJump"))
        {
            if (!CheckGround())
            {
                Jump();
            }
        }

        if (Input.GetMouseButtonDown(0)|| Input.GetButtonDown("LeftBumper"))
        {
            Possess();
        }

        if (Input.GetMouseButtonDown(1) || Input.GetButtonDown("RightBumper"))
        {
            UnPossess();
        }
    }

    void Move()
    {
        movement = Vector3.Lerp(movement, Vector3.ClampMagnitude(camTransform.right*input.x + camTransform.forward* input.y, 1f) * speed, moveLerpSpeed);
    }

    void Stop()
    {
        movement = Vector3.Lerp(movement, Vector3.zero, stopLerpSpeed);
    }

    void Turn()
    {
        direction = new Vector3(0, Mathf.Atan2(-input.y, input.x) * 180 / Mathf.PI-90 + camTransform.eulerAngles.y, 0);

        float step = rotationSpeed;
		
        Quaternion turnRotation = Quaternion.Euler(0f, direction.y, 0f);
				
        transform.transform.localRotation = Quaternion.RotateTowards(transform.localRotation, turnRotation, step );
    }

    void Jump()
    {
        rb.velocity += new Vector3(0,jumpForce,0);
    }

    bool CheckGround()
    {
        //if(Physics.Raycast(transform.position, Vector3.down, checkGroundDistance))
        RaycastHit hit;
        if(Physics.SphereCast(transform.position, .45f,Vector3.down, out hit, checkGroundDistance))
        {
            return false;
        }
            return true;
    }

    void DetectObstacle()
    {
        if (Physics.Raycast(footTransform.position, transform.forward, 1))
        {
            print("Foot");
            if (Physics.Raycast(hipsTransform.position, transform.forward, 1))
            {
                print("hips");
                if (Physics.Raycast(headTransform.position, transform.forward, 1))
                {
                    print("head");
                }
            }
        }
    }

    void Possess()
    {
        RaycastHit hit;
        if(Physics.SphereCast(transform.position, .45f,Vector3.down, out hit, checkGroundDistance))
        {
            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                rb.isKinematic = true;
            }
            
            
            GameObject _block = hit.collider.gameObject;

            Vector3 _offset = hit.point - _block.transform.position;
            _offset.x = 0;
            _offset.z = 0;
            
            offsets.Add(_offset);
            
            objects.Add(_block);

            lastObject.transform.localPosition += _offset;
            _block.transform.SetParent(visualsObject);
            
            _block.transform.localPosition = new Vector3(_block.transform.localPosition.x, 0, _block.transform.localPosition.z);
            
            lastObject.SetParent(_block.transform);

            lastObject = _block.transform;

            transform.position -= offsets[offsets.Count - 1];
        }
    }

    void UnPossess()
    {
        if (objects.Count < 2)
        {
            print("No Blocks to remove");
            return;
        }
        
        
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, .45f, Vector3.down, out hit, checkGroundDistance))
        {


            Transform _object = objects[objects.Count - 2].transform;

            print(_object.gameObject);
            print(lastObject.gameObject);
            
            lastObject.parent = null;
     
            _object.SetParent(visualsObject);

            lastObject.position = new Vector3(lastObject.position.x, hit.point.y, lastObject.position.z);

            _object.localPosition = Vector3.zero;

            transform.position += offsets[offsets.Count - 1];
            
            offsets.RemoveAt(offsets.Count - 1);

            objects.Remove(lastObject.gameObject);

            //lastObject = _object;
            lastObject = objects[objects.Count - 1].transform;
                
            print(lastObject.gameObject);

            //Jump();
        }
    }
}
