using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyLerper : MonoBehaviour
{
    public float fps = 1;

    public Transform target;

    public bool lerping;

    public bool lerpLeg;

    public Leg[] legs;
    public bool rightLeg;

    public Rigidbody rb;
    void Start()
    {
        StartCoroutine(UpdateBody());
        StartCoroutine(UpdateLegs());
    }

    private void Update()
    {
        fps += Input.mouseScrollDelta.y;
        Mathf.Clamp(fps, 1, 100);
    }

    IEnumerator UpdateBody()
    {
        while (true)
        {
            transform.SetPositionAndRotation(target.position, target.rotation);
            if (lerping)
            {
                yield return new WaitForSeconds(1/fps);
            }
            else
            {
                yield return null;
            }
        }
    }

    IEnumerator UpdateLegs()
    {
        while (true)
        {

            if (rightLeg)
            {
                legs[0].UpdateLeg(rb.velocity);
            }
            else
            {
                legs[1].UpdateLeg(rb.velocity);
            }

            rightLeg = !rightLeg;
            
            
            if (lerpLeg)
            {
                yield return new WaitForSeconds(1/fps);
            }
            else
            {
                yield return null;
            }
        }
    }

    
}
