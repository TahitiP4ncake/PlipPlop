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

    public float climbForce;


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

    public Transform orientationTransform;

    public float sideTilt;
    public float frontTilt;

    public float maxTiltAngle;

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


    public bool controlled = true;

    public Transport lastTransport;

    private Vector3 groundPosition;

    public Collider bodyCol;

    public Ground currentGround;


    private Vector3 groundScale;
    
    //TALKING

    public bool talking;
    
    //Props

    public GameObject[] props;
    public int propsIndex;
    
    //COTTON
    
    [Header("Cotton")]
    public bool cotton;

    public float cottonForce;

    public float cottonDownSpeedMax;


    public float idleTime;

    private float idleTimer;


    public bool sitting;
 
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
        if (!controlled )
        {
            orientationTransform.localEulerAngles = new Vector3(0,0,0);
            return;
        }

        if (talking)
        {
            Stop();
            if (walking)
            {
                walking = false;
                anim.SetBool("Walking", false);
            }
            orientationTransform.localEulerAngles = new Vector3(0,0,0);
            movement.y = rb.velocity.y;
            rb.velocity = Vector3.Lerp(rb.velocity, movement, Mathf.Clamp01(influence));
            return;
        }
        
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
        
        //Inclinaison du personnage à tweaker en fonction de la taille
        orientationTransform.localEulerAngles = new Vector3(Mathf.Lerp(orientationTransform.localEulerAngles.x, Mathf.Clamp(movement.magnitude*frontTilt, -maxTiltAngle, maxTiltAngle), .5f),0,orientationTransform.localEulerAngles.z);
        
        if (IsGround)
        {
            orientationTransform.localEulerAngles = new Vector3(0,0,0);
            return;
        }
        //Apply Inputs
        if (( input.x != 0 || input.y != 0 ) && !sitting)
        {
            if (!walking)
            {
                walking = true;
                anim.SetBool("Walking", true);
            }
            Move();
            if (!IsGround) Turn();
        }
        else
        {
            if (walking)
            {
                walking = false;
                anim.SetBool("Walking", false);
            }
            orientationTransform.localEulerAngles = new Vector3(orientationTransform.localEulerAngles.x,0,0);
            Stop();
        }

        if (CheckGround())
        {
            if (!cotton)
            {
                rb.velocity += Vector3.down * gravityStrength;
            }
            else
            {
                rb.velocity +=Vector3.up* Time.fixedDeltaTime * cottonForce;
                Vector3 _movement = rb.velocity;
                _movement.y = Mathf.Clamp(_movement.y, cottonDownSpeedMax, Mathf.Infinity);
                rb.velocity = Vector3.Lerp(rb.velocity,_movement,Time.fixedDeltaTime*2);
            }
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

        if(lerpVisuals) LerpVisuals();

//YKILL TEMP
        if (transform.position.y < YKILL)
        {
            transform.position = currentGround.respawnTransform.position;
            //transform.position = new Vector3(0,0,0);
            rb.velocity = Vector3.zero;
            Jump();
            Manager.SINGLETON.PlaySound("plouf", .6f);
            print("Repop");
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
        if (talking)
        {
            return;
        }

        if (!sitting)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("LeftBumper"))
            {
                Possess();
                idleTimer = 0;
            }

            if (Input.GetMouseButtonDown(1) || Input.GetButtonDown("RightBumper"))
            {
                UnPossess();
                idleTimer = 0;
            }
        }


        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");


        if (IsGround || !controlled)
        {
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("GamepadJump"))
        {
            if (!CheckGround() || sitting)
            {
                Jump();
                idleTimer = 0;
            }
            else
            {
                pressedJump = true;
                jumpBufferTimer = 0;   
            }
        }


        if (Input.GetButtonDown("GamepadY"))
        {
            if (props[propsIndex] != null)
            {
                props[propsIndex].SetActive(false);
            }
            propsIndex++;

            if (propsIndex > props.Length - 1) propsIndex = 0;
            if (props[propsIndex] != null) props[propsIndex].SetActive(true);
        }
        
        if (sitting == false)
        {
            idleTimer += Time.deltaTime;
            if (idleTimer > idleTime)
            {
                idleTimer = 0;
                anim.SetTrigger("Sit");
                sitting = true;
            }
        }
    }

    void Move()
    {
        movement = Vector3.Lerp(movement, Vector3.ClampMagnitude(camTransform.right*input.x + camTransform.forward* input.y, 1f) * speed, moveLerpSpeed);
        idleTimer = 0;
        DetectObstacle();
    }

    void Stop()
    {
        movement = Vector3.Lerp(movement, Vector3.zero, stopLerpSpeed);
    }

    void Turn()
    {
        direction = new Vector3(0, Mathf.Atan2(-input.y, input.x) * 180 / Mathf.PI-90 + camTransform.eulerAngles.y, 0);
        float _rotation = Mathf.DeltaAngle (transform.eulerAngles.y, direction.y);
        float step = rotationSpeed * Mathf.Abs (_rotation)/180;
        _rotation = (_rotation > 180) ? _rotation - 360 : _rotation;
        orientationTransform.localEulerAngles = new Vector3(orientationTransform.localEulerAngles.x,0,Mathf.Clamp(_rotation*sideTilt,  -maxTiltAngle, maxTiltAngle));
        Quaternion turnRotation = Quaternion.Euler(0f, direction.y, 0f);
        transform.transform.localRotation = Quaternion.RotateTowards(transform.localRotation, turnRotation, step );
    }

    void Jump()
    {
        if (sitting) sitting = false;
        anim.SetTrigger("Jump");
        anim.SetBool("Grounded", false);
        rb.velocity = new Vector3(rb.velocity.x,jumpForce, rb.velocity.z);
        landDust.Play();
       Manager.SINGLETON.PlaySound("jump",.2f);
    }

    bool CheckGround()
    {
        //if(Physics.Raycast(transform.position, Vector3.down, checkGroundDistance))
        RaycastHit hit;
        if(Physics.SphereCast(transform.position, .45f,Vector3.down, out hit, checkGroundDistance))
        {
            if (!grounded)
            {
                grounded = true;
                anim.SetBool("Grounded", true);
                landDust.Play();
                Manager.SINGLETON.PlaySound("land",.15f);
            }
            return false;
        }

        if (grounded)
        {
            grounded = false;
            anim.SetBool("Grounded", false);
        }
        return true;
    }

    
    //ULTRA TEMP PTN TODO
    void DetectObstacle()
    {
        if (Physics.Raycast(footTransform.position, -footTransform.forward, .5f))
        {
            print("Foot");
            if (!Physics.Raycast(hipsTransform.position, -footTransform.forward, .5f))
            {
                
                print("climb!");
                
                rb.velocity+= new Vector3(0,climbForce,0);
                /*
                if (Physics.Raycast(headTransform.position, transform.forward, .2f))
                {
                    print("head");
                }
                */
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
            
            anim.SetTrigger("Plip");
            
            emotions.ChangeEmotion(Emotion.Smile);

            if (hit.collider.gameObject.CompareTag("Ground"))
            {
                print("GROUND");
                anim.SetBool("IsGround", true);
                rb.isKinematic = true;
                IsGround = true;

                groundPosition = hit.collider.gameObject.transform.position;

                groundScale = hit.collider.gameObject.transform.localScale;

                //Play stuck Animation
                //anim.SetTrigger("Grounded");
                

            }
            else if (hit.collider.gameObject.CompareTag("No"))
            {
                //print("NO!");

                influence = -.5f;
                rb.velocity += (transform.forward + Vector3.up) * 10;
                
                emotions.ChangeEmotion(Emotion.Sad);
                
                return;        
            }
            else if(hit.collider.gameObject.CompareTag("Boat"))
            {
                //Attach to boat

               // print("BOAT");

                bodyCol.enabled = false;
                
                anim.SetBool("IsGround", true);

                
                rb.isKinematic = true;
                
                transform.parent = hit.collider.transform;
                
                transform.localPosition-=new Vector3(0,1,0);

                controlled = false;

                lastTransport = hit.collider.GetComponent<Transport>();
                lastTransport.GetIn();
                
                Manager.SINGLETON.PlaySound("plip",.4f);


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
                //print(cameraOffset);
                
                lastObject.transform.parent = null;//

                transform.position = _block.transform.position;//
                
                _block.transform.SetParent(visualsObject);
                
                _block.transform.localEulerAngles = new Vector3(0,_block.transform.localEulerAngles.y,0);
            
                _block.transform.localPosition = new Vector3(_block.transform.localPosition.x, 0, _block.transform.localPosition.z);//
            
                
                lastObject.SetParent(_block.transform);

                //lastObject.transform.localPosition -= _offset/2;
                lastObject.localPosition = new Vector3(lastObject.localPosition.x, _offset.y, lastObject.localPosition.z);
                
                
                lastObject = _block.transform;
                
                transform.position += new Vector3(0,.1f,0);
                
                Manager.SINGLETON.PlaySound("plip",.4f);

                Block _blockType = _block.GetComponent<Block>();

                if (_blockType)
                {
                    if (_blockType.type == BlockType.Cotton)
                    {
                        cotton = true;
                    }
                }


                //hit.collider.gameObject.GetComponent<MeshCollider>().convex = true;

                //transform.position -= offsets[offsets.Count - 1]; //
            }
            else
            {
                //IS GROUND
                
                hit.collider.gameObject.GetComponent<MeshCollider>().convex = true;

                
                orientationTransform.localEulerAngles = new Vector3(0,0,0);

                //TEMP
                //visualsObject.transform.localPosition = new Vector3(0,0,-1f);


                //lastObject.transform.parent = null;
                
                transform.position -= new Vector3(0,1.23f,0);
                
                _block.transform.SetParent(visualsObject);
                
               // _block.transform.localEulerAngles = new Vector3(0,_block.transform.localEulerAngles.y,0);


                lastObject.SetParent(_block.transform);

                lastObject = _block.transform;
                
                Manager.SINGLETON.PlaySound("plip",.4f);

            }
        }
    }

    void UnPossess()
    {
        if (!controlled)
        {
            anim.SetTrigger("Plop");

            //print("FREE!");
            
            bodyCol.enabled = true;


            rb.isKinematic = false;

            transform.localPosition+=new Vector3(0,1,0);

            transform.parent = null;
                

            controlled = true;
            
            lastTransport.GetOut();
            lastTransport = null;
            
            anim.SetBool("IsGround", false);
            emotions.ChangeEmotion(Emotion.Smile);
            
            Manager.SINGLETON.PlaySound("plop",.4f);

            return;
        }
        
        
        if (objects.Count < 2)
        {
            emotions.ChangeEmotion(Emotion.Sad);
            print("No Blocks to remove");
            return;
        }
        
        anim.SetTrigger("Plop");


       
        
        

        if (IsGround)
        {
            Transform _object = objects[objects.Count - 2].transform;

            //print(_object.gameObject);
            //print(lastObject.gameObject);
            
            lastObject.parent = null;

            transform.position = _object.transform.position + new Vector3(0, .5f, 0);

            
            _object.SetParent(visualsObject);


            _object.localPosition = Vector3.zero;
            

            
            
            //transform.position += offsets[offsets.Count - 1];

            //?
            //lastObject.transform.localEulerAngles = new Vector3(0, lastObject.transform.localEulerAngles.y, 0);

          
            
            offsets.RemoveAt(offsets.Count - 1);

            objects.Remove(lastObject.gameObject);

            lastObject.transform.localScale = groundScale;

            transform.localScale = Vector3.one;

            //lastObject = _object;
            lastObject.transform.position = groundPosition;

            lastObject.GetComponent<MeshCollider>().convex = false;
            
            lastObject = objects[objects.Count - 1].transform;
                
            //TEMP
            //visualsObject.transform.localPosition = Vector3.zero;

            
            rb.isKinematic = false;
            IsGround = false;
            
            emotions.ChangeEmotion(Emotion.Smile);
            
            anim.SetBool("IsGround", false);
            
            Manager.SINGLETON.PlaySound("plop",.4f);



            return;
        }
        
        
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, .45f, Vector3.down, out hit, checkGroundDistance))
        {

            
            Block _blockType = lastObject.GetComponent<Block>();

            if (_blockType)
            {
                if (_blockType.type == BlockType.Cotton)
                {
                    cotton = false;
                }
            }
            
            

            Transform _object = objects[objects.Count - 2].transform;

            
            lastObject.parent = null;
     
            _object.SetParent(visualsObject);

            lastObject.position = new Vector3(lastObject.position.x, hit.point.y, lastObject.position.z);

            _object.localPosition = new Vector3(_object.localPosition.x, 0, _object.localPosition.z);

            _object.transform.parent = null;

            lastObject.transform.localEulerAngles = new Vector3(0, lastObject.transform.localEulerAngles.y, 0);
            
            transform.position += offsets[offsets.Count - 1]/2;
            
            transform.position = new Vector3(_object.transform.position.x, transform.position.y, _object.transform.position.z);//
            
            _object.transform.SetParent(visualsObject);
            _object.transform.localPosition = Vector3.zero;
            
            cameraOffset += offsets[offsets.Count - 1].y;
            
            //print(cameraOffset);
            
            offsets.RemoveAt(offsets.Count - 1);

            objects.Remove(lastObject.gameObject);

            lastObject.localScale = Vector3.one;

            
            lastObject.gameObject.GetComponent<Block>().Drop(true);
            
            
            //lastObject.GetComponent<MeshCollider>().convex = false;

            
            lastObject = objects[objects.Count - 1].transform;
                
            emotions.ChangeEmotion(Emotion.Smile);
            
            Manager.SINGLETON.PlaySound("plop",.4f);

            
            //Uniquement si on release tout les cubes
            //Jump();
        }
        else
        {
            if (rb.velocity.y < float.Epsilon && rb.velocity.y > -float.Epsilon)
            {
                
                //A FIX PARCE QUE ANIM maintenant
                print("PLAYER IS STATIC ENOUGH TO DEPOP");
                
                Transform _object = objects[objects.Count - 2].transform;

            
                lastObject.parent = null;
     
                _object.SetParent(visualsObject);

                //lastObject.position = new Vector3(lastObject.position.x, hit.point.y, lastObject.position.z);

                _object.localPosition = new Vector3(_object.localPosition.x, 0, _object.localPosition.z);

                _object.transform.parent = null;
            
                lastObject.transform.localEulerAngles = new Vector3(0, lastObject.transform.localEulerAngles.y, 0);
            
                transform.position += offsets[offsets.Count - 1]/2;
            
                transform.position = new Vector3(_object.transform.position.x, transform.position.y, _object.transform.position.z);//
            
                _object.transform.SetParent(visualsObject);
                _object.transform.localPosition = Vector3.zero;

                transform.position += offsets[offsets.Count - 1];
            
                offsets.RemoveAt(offsets.Count - 1);

                objects.Remove(lastObject.gameObject);

                lastObject.localScale = Vector3.one;

                
                lastObject.gameObject.GetComponent<Block>().Drop(false);

            
                lastObject.GetComponent<MeshCollider>().convex = false;

                
                lastObject = objects[objects.Count - 1].transform;
                
                emotions.ChangeEmotion(Emotion.Smile);
                
                Manager.SINGLETON.PlaySound("plop",.4f);

            }
            else
            {
                print("STOP MOVING");
                
                // ça c'est encore un problème , faut que je trouve comment détecter ça mieux
                // TODO
                
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
                anim.SetBool("Grounded", true);
                
               // Manager.SINGLETON.PlaySound("land",.4f);
            }
        }
    }

    public void FootStep(int _i)
    {

        if (grounded)
        {
            //walkingDust[_i].Play();

            Manager.SINGLETON.PlaySound("step",.2f);
        }
    }

    public void ChangeGround(Ground _ground)
    {
        if (currentGround != null)
        {
            currentGround.currentGround = false;

        }
        
        currentGround = _ground;

    }

    public void StartTalking(Transform _position)
    {
        
        
        talking = true;

        StartCoroutine(LookAtTarget(_position));

    }
    
    IEnumerator LookAtTarget(Transform _target)
    {
        float _y = 0;
        
        Vector3 relativePos = transform.position - _target.position;
        relativePos.y = 0;

        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

        while (_y < 1)
        {

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _y);
            _y += Time.deltaTime*3;
            yield return null;
        }

    }

    public void StopTalking()
    {
        talking = false;
    }


}
