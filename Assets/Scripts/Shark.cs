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

    void Start()
    {
        startPosition = transform.position;
    }
    
    void Update()
    {
        Vector3 _pos = GetPosition(Time.time);

        transform.position = startPosition + _pos;

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
