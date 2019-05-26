using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BodyAnimations : MonoBehaviour
{
    public float fps = 1;

    public bool lerpLeg;

    public Leg[] legs;
    public bool rightLeg;

    public Vector3 velocity = Vector3.zero;
    
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
                legs[0].UpdateLeg(velocity);
            }
            else
            {
                legs[1].UpdateLeg(velocity);
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
