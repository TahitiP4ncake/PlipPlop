using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Shark : MonoBehaviour
{
    public float timeBeforeMoving;

    public float radius;

    public float speed;

    public int xRatio;
    public int yRatio;

    private Vector3 startPosition;

    public bool shaking;
    
    void Start()
    {
        startPosition = transform.position;

        speed = Random.Range(speed / 2, speed * 2);

        radius = Random.Range(radius / 2, radius * 2);

        if (Random.Range(0, 2) > 0)
        {
            speed *= -1;
        }
    }
    
    void Update()
    {
        Vector3 _pos = GetPosition(Time.time);

        transform.position = startPosition + _pos;

        if (shaking)
        {
            transform.position = Vector3.Lerp(transform.position, transform.position + Random.insideUnitSphere *2,
                Time.deltaTime * 5);
        }
        

        Vector3 _target = GetPosition(Time.time + Time.deltaTime);

        transform.forward = (_target - _pos).normalized;

    }

    Vector3 GetPosition(float _time)
    {

        float x = radius * Mathf.Cos(_time * speed * xRatio);
        float y = radius * Mathf.Sin(_time * speed * yRatio);


        return new Vector3(x, 0, y);
    }

}
