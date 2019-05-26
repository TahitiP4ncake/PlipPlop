using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyLerper : MonoBehaviour
{
    public float fps = 1;

    public bool lerpLeg;

    public Leg[] legs;
    public bool rightLeg;

    public Rigidbody rb;
    
    void Start()
    {
        StartCoroutine(UpdateLegs());
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
