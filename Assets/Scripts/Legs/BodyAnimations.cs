using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BodyAnimations : MonoBehaviour
{
    public float legFps = 4;

    public bool lerpLeg;

    public Leg[] legs;
    public bool rightLeg;

    public Vector3 velocity = Vector3.zero;
    
    //Body

    public bool lerpBody = true;
    public Transform body;
    public float bodyAmp = .05f;
    public float bodyTilt = 1;
    public float bodyTurn = 10;

    private Vector3 bodyStartPosition;
    Vector3 startLocalEulerAngles;
    private bool up;
    
    
    void Start()
    {
        bodyStartPosition = body.localPosition;
        startLocalEulerAngles = body.localEulerAngles;
        StartCoroutine(UpdateLegs());
        StartCoroutine(UpdateBody());
    }

    IEnumerator UpdateBody()
    {
        while (true)
        {
            if (lerpBody)
            {
                
                
                if (up)
                {
                    body.localPosition = bodyStartPosition - new Vector3(0,bodyAmp,0) * (Vector3.ClampMagnitude(velocity,1).magnitude +.1f);
                }
                else
                {
                    body.localPosition = bodyStartPosition + new Vector3(0,bodyAmp,0) * (Vector3.ClampMagnitude(velocity,1).magnitude +.1f);
                }
                
                up = !up;

                yield return new WaitForSeconds(1/legFps/2);
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
                legs[0].UpdateLeg(velocity);
                body.localEulerAngles = startLocalEulerAngles+ new Vector3(Random.Range(-bodyTilt,bodyTilt),Random.Range(-bodyTurn,-bodyTurn + .5f),Random.Range(-bodyTilt,bodyTilt))* (Vector3.ClampMagnitude(velocity,1).magnitude + .1f);

            }
            else
            {
                legs[1].UpdateLeg(velocity);
                body.localEulerAngles = startLocalEulerAngles + new Vector3(Random.Range(-bodyTilt,bodyTilt),Random.Range(bodyTurn,bodyTurn -5f),Random.Range(-bodyTilt,bodyTilt))* (Vector3.ClampMagnitude(velocity,1).magnitude + .1f);

            }
            rightLeg = !rightLeg;

            
            
            
            if (lerpLeg)
            {
                yield return new WaitForSeconds(1/legFps);
            }
            else
            {
                yield return null;
            }
        }
    }

    
}
