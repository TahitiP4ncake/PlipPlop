using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

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


    [Space] 
    
    public float jumpBufferTime;


    [Space] 
    
    public Transform cameraZObject;
    public Vector2 cameraZ;

    private float cameraOffset;

    public PostProcessVolume pp;
    public PostProcessProfile profileRef;
    private PostProcessProfile profile;

    [Space]
    private DepthOfField depth;

    public float closeDistance;
    public float farDistance;
    
    public float closeFocal;
    public float farFocal;

    public float closeAperture;
    public float farAperture;

    [Space] 
    
    public Animator anim;
    public bool walking;


    [Space] 
    
    
    public Emotions emotions;

    [Space] 
    [Header("Particles")] 
    [Space] 
    
    public ParticleSystem landDust;

    public ParticleSystem[] walkingDust;
    
    //Internal Data

    private Vector2 input;
    
    private Vector3 movement;
    private Vector3 direction;

    private float gravityValue;
    
    
    
    [Space]

    public bool IsGround;

    public bool grounded;
    public bool lerpVisuals;

    private float jumpBufferTimer;
    private bool pressedJump;

    private float influence = 1;
    
    
    

    void Start()
    {
        if(lerpVisuals)
        visualsObject.parent = null;

        lastObject = visualsObject.GetChild(0);

        profile = Instantiate(profileRef);
        pp.profile = profile;
        
        profile.TryGetSettings(out depth);

    }
    
    void Update()
    {
        CheckInputs();
        
        JumpBuffer();
    }

    void FixedUpdate()
    {
        //CAMERA ZOOM
        if (!IsGround)
        {
            cameraZObject.localPosition = new Vector3(0,0,Mathf.Lerp(cameraZObject.localPosition.z,cameraZ.x + cameraOffset, .3f));
//            cameraZObject.localPosition = new Vector3(0,0,Mathf.Lerp(cameraZObject.localPosition.z, Mathf.Clamp(cameraZ.x + cameraOffset,cameraZ.x,cameraZ.y), .3f));


            depth.focalLength.value = Mathf.Lerp(depth.focalLength.value, closeFocal, .3f);
            depth.focusDistance.value = Mathf.Lerp(depth.focusDistance.value, closeDistance, .3f);
            depth.aperture.value = Mathf.Lerp(depth.aperture.value, closeAperture, .3f);
            
            
        }
        else
        {
            cameraZObject.localPosition = new Vector3(0,0,Mathf.Lerp(cameraZObject.localPosition.z, cameraZ.y, .2f));
            
            depth.focalLength.value = Mathf.Lerp(depth.focalLength.value, farFocal, .2f);
            depth.focusDistance.value = Mathf.Lerp(depth.focusDistance.value, farDistance, .2f);
            depth.aperture.value = Mathf.Lerp(depth.aperture.value, farAperture, .2f);

        }
        
        
        
        
        if (IsGround)
        {
            return;
        }
        
        
        //Apply Inputs
        
        if (input.x != 0 || input.y != 0)
        {

            if (!walking)
            {
                walking = true;
                anim.SetBool("Walking", true);
            }
            
            Move();
            
            if (!IsGround)
            {
                Turn();
            }
        }
        else
        {
            if (walking)
            {
                walking = false;
                anim.SetBool("Walking", false);
            }
            
            Stop();
        }

        if (CheckGround())
        {
            rb.velocity += Vector3.down * gravityStrength;
        }
        else
        {
            if (pressedJump)
            {
                pressedJump = false;
                Jump();
            }
        }
        
        movement.y = rb.velocity.y;

        rb.velocity = Vector3.Lerp(rb.velocity, movement, Mathf.Clamp01(influence));
        
        if (lerpVisuals)
            LerpVisuals();

//YKILL TEMP
        if (transform.position.y < YKILL)
        {
            transform.position = new Vector3(0,10,0);
            rb.velocity = Vector3.zero;
            grounded = false;
        }


        
        
        UpdateInfluence();
    }

    void UpdateInfluence()
    {
        if (influence < 1)
        {
            influence += Time.fixedDeltaTime;

            if (influence >= 1)
            {
                influence = 1;
            }
        }
    }

    void JumpBuffer()
    {
        if (pressedJump)
        {
            jumpBufferTimer += Time.deltaTime;

            if (jumpBufferTimer >= jumpBufferTime)
            {
                pressedJump = false;
            }
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
            else
            {
                pressedJump = true;
                jumpBufferTimer = 0;
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
        rb.velocity = new Vector3(rb.velocity.x,jumpForce, rb.velocity.z);
        landDust.Play();
    }

    bool CheckGround()
    {
        //if(Physics.Raycast(transform.position, Vector3.down, checkGroundDistance))
        RaycastHit hit;
        if(Physics.SphereCast(transform.position, .45f,Vector3.down, out hit, checkGroundDistance))
        {

            return false;
        }

        if (grounded)
        {
            grounded = false;
            print("OFF THE GROUND");
            
            //anim.SetTrigger("Air");
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

            
            
            if (IsGround)
            {
                return;
            }
            
            emotions.ChangeEmotion(Emotion.Smile);

            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                rb.isKinematic = true;
                IsGround = true;
                
                
                //Play stuck Animation
                //anim.SetTrigger("Grounded");
            }
            else if (hit.collider.gameObject.CompareTag("No"))
            {
                print("NO!");

                influence = -.5f;
                rb.velocity += (transform.forward + Vector3.up) * 10;
                
                emotions.ChangeEmotion(Emotion.Sad);
                
                return;        
            }
            
            
            GameObject _block = hit.collider.gameObject;

            Vector3 _pos = _block.transform.position;

            Vector3 _offset = hit.point - _block.transform.position;
            _offset.x = 0;
            _offset.z = 0;
            
            offsets.Add(_offset);
            
            objects.Add(_block);

            if (!IsGround)
            {
                //lastObject.transform.localPosition += _offset;

                cameraOffset -= _offset.y;
                print(cameraOffset);
                
                lastObject.transform.parent = null;//

                transform.position = _block.transform.position;//
                
                _block.transform.SetParent(visualsObject);
            
                _block.transform.localPosition = new Vector3(_block.transform.localPosition.x, 0, _block.transform.localPosition.z);//
            
                
                lastObject.SetParent(_block.transform);

                //lastObject.transform.localPosition -= _offset/2;
                   lastObject.localPosition = new Vector3(lastObject.localPosition.x, _offset.y, lastObject.localPosition.z);
                
                
                lastObject = _block.transform;

                //transform.position -= offsets[offsets.Count - 1]; //
            }
            else
            {
                
                //TEMP
                visualsObject.transform.localPosition = new Vector3(0,0,-1f);
                
                _block.transform.SetParent(visualsObject);

                lastObject.SetParent(_block.transform);

                lastObject = _block.transform;
            }
        }
    }

    void UnPossess()
    {
        if (objects.Count < 2)
        {
            emotions.ChangeEmotion(Emotion.Sad);
            print("No Blocks to remove");
            return;
        }

        if (IsGround)
        {
            Transform _object = objects[objects.Count - 2].transform;

            //print(_object.gameObject);
            //print(lastObject.gameObject);
            
            lastObject.parent = null;
     
            _object.SetParent(visualsObject);


            _object.localPosition = Vector3.zero;

            //transform.position += offsets[offsets.Count - 1];

          
            
            offsets.RemoveAt(offsets.Count - 1);

            objects.Remove(lastObject.gameObject);

            //lastObject = _object;
            lastObject = objects[objects.Count - 1].transform;
                
            //TEMP
            visualsObject.transform.localPosition = Vector3.zero;

            
            rb.isKinematic = false;
            IsGround = false;
            
            emotions.ChangeEmotion(Emotion.Smile);

            return;
        }
        
        
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, .45f, Vector3.down, out hit, checkGroundDistance))
        {


            Transform _object = objects[objects.Count - 2].transform;

            
            lastObject.parent = null;
     
            _object.SetParent(visualsObject);

            lastObject.position = new Vector3(lastObject.position.x, hit.point.y, lastObject.position.z);

            _object.localPosition = new Vector3(_object.localPosition.x, 0, _object.localPosition.z);

            _object.transform.parent = null;
            
            
            
            transform.position += offsets[offsets.Count - 1]/2;
            
            transform.position = new Vector3(_object.transform.position.x, transform.position.y, _object.transform.position.z);//
            
            _object.transform.SetParent(visualsObject);
            _object.transform.localPosition = Vector3.zero;
            
            cameraOffset += offsets[offsets.Count - 1].y;
            
            print(cameraOffset);
            
            offsets.RemoveAt(offsets.Count - 1);

            objects.Remove(lastObject.gameObject);

            lastObject.gameObject.GetComponent<Block>().Drop(true);
            
            lastObject = objects[objects.Count - 1].transform;
                
            emotions.ChangeEmotion(Emotion.Smile);

            
            //Uniquement si on release tout les cubes
            //Jump();
        }
        else
        {
            if (rb.velocity.y < float.Epsilon && rb.velocity.y > -float.Epsilon)
            {
                
                print("PLAYER IS STATIC ENOUGH TO DEPOP");
                
                Transform _object = objects[objects.Count - 2].transform;

            
                lastObject.parent = null;
     
                _object.SetParent(visualsObject);

                //lastObject.position = new Vector3(lastObject.position.x, hit.point.y, lastObject.position.z);

                _object.localPosition = new Vector3(_object.localPosition.x, 0, _object.localPosition.z);

                _object.transform.parent = null;
            
            
            
                transform.position += offsets[offsets.Count - 1]/2;
            
                transform.position = new Vector3(_object.transform.position.x, transform.position.y, _object.transform.position.z);//
            
                _object.transform.SetParent(visualsObject);
                _object.transform.localPosition = Vector3.zero;

                transform.position += offsets[offsets.Count - 1];
            
                offsets.RemoveAt(offsets.Count - 1);

                objects.Remove(lastObject.gameObject);

                lastObject.gameObject.GetComponent<Block>().Drop(false);
            
                lastObject = objects[objects.Count - 1].transform;
                
                emotions.ChangeEmotion(Emotion.Smile);

            }
            else
            {
                print("STOP MOVING");
                
                emotions.ChangeEmotion(Emotion.Sad);

            }
        }
    }

    void OnCollisionEnter()
    {
        if (!CheckGround())
        {
            if (grounded == false)
            {
                print("GROUNDED");
                //anim.SetTrigger("Land");
                landDust.Play();
                grounded = true;
            }
        }
    }

    public void FootStep(int _i)
    {
        
        if(grounded)
        walkingDust[_i].Play();
    }
    
}
