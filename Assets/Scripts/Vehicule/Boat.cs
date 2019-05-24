using UnityEngine;

public class Boat : Vehicule
{
    private Rigidbody rb;
    private Vector3 movement;
    private Vector3 direction;
    private Vector3 input;

    private bool on;

    [Header("Settings")]
    public float speed;
    public float rotationSpeed;
    public float lerpSpeed;
    public float slowDownLerp;
    public float sideTilt;
    public float frontTilt;
    public float maxTiltAngle;
    public GameObject fx;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(!on) return;

        if (input.y != 0 || input.x !=0)
        {
            Move();
            Turn();
            if (fx.activeSelf == false) fx.SetActive(true);
        }
        else
        {
            if (fx.activeSelf == true) fx.SetActive(false);
            float _angle = (transform.localEulerAngles.z > 180) ? transform.localEulerAngles.z - 360 : transform.localEulerAngles.z;
            transform.localEulerAngles = new Vector3(transform.localEulerAngles.x,transform.localEulerAngles.y,Mathf.Clamp(Mathf.Lerp(_angle, 0*sideTilt,.5f),  -maxTiltAngle, maxTiltAngle));
            SlowDown();
        }
        transform.localEulerAngles = new Vector3(Mathf.Clamp(movement.magnitude * -frontTilt, -maxTiltAngle, maxTiltAngle),transform.localEulerAngles.y,transform.localEulerAngles.z);
        movement.y = rb.velocity.y;
        rb.velocity = movement;
    }

    public override void Operate(Pilote pilote)
    {
        base.Operate(pilote);
        input = pilote.inputs.direction;
    }
    public override void On()
    {
        base.On();
        rb.isKinematic = false;
        input = Vector2.zero;
        on = true;
    }
    public override void Off()
    {
        base.Off();
        rb.isKinematic = true;
        fx.SetActive(false);
        on = false;
    }

    void Move()
    {
        movement = Vector3.Lerp(movement, transform.forward * speed, lerpSpeed);
    }

    void SlowDown()
    {
        movement = Vector3.Lerp(movement, Vector3.zero, slowDownLerp);
    }

    void Turn()
    {
        direction = new Vector3(0, Mathf.Atan2(input.y, -input.x) * 180 / Mathf.PI-90 + Camera.main.transform.eulerAngles.y, 0);
        float _rotation = Mathf.DeltaAngle (transform.eulerAngles.y, direction.y);
        float step = rotationSpeed * Mathf.Abs (_rotation)/180;
        _rotation = (_rotation > 180) ? _rotation - 360 : _rotation;
        float _angle = (transform.localEulerAngles.z > 180) ? transform.localEulerAngles.z - 360 : transform.localEulerAngles.z;
        transform.localEulerAngles = new Vector3(0,transform.localEulerAngles.y,Mathf.Clamp(Mathf.Lerp(_angle, _rotation*sideTilt,.2f),  -maxTiltAngle, maxTiltAngle));
        Quaternion turnRotation = Quaternion.Euler(0f, direction.y, 0f);
        transform.transform.localRotation = Quaternion.RotateTowards(transform.localRotation, turnRotation, step);    
    }
}
