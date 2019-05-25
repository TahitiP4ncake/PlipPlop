using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : MonoBehaviour
{
    public Transform foot;
    public Transform hip;
    public Transform knee;

    private RaycastHit hit;

    public bool lerping;

    public float fps = 4;

    public float forwardDistance = 1;

    public float rayDistance = 2;

    public float maxFootDistance = 2;

    public float kneeNoise = .2f;
    public float kneeVelInfluence = 0;

    public PlayerMovement player;
    
    private void Start()
    {
        foot.transform.parent = null;
        //knee.transform.parent = null;
        //StartCoroutine(UpdateBody());
    }

    private void Update()
    {
       // hip.eulerAngles = foot.eulerAngles;
        knee.eulerAngles = foot.eulerAngles;
        //knee.LookAt(foot);
        //knee.localEulerAngles = new Vector3(0, 0, 90);
        //knee.rotation = Quaternion.Slerp(foot.rotation, Quaternion.LookRotation(hip.position),.5f);
        //knee.rotation = Quaternion.LookRotation(foot.position, transform.forward);
        //hip.LookAt(foot);
        //hip.localEulerAngles += new Vector3(90,-90,180);
        

        if (Vector3.Distance(foot.position, hip.position) > maxFootDistance)
        {
            foot.position = hip.position + Vector3.down/1.5f  + GetNoise();
            foot.SetParent(hip);
            //knee.SetParent(hip);
            
            UpdateKnee(player.rb.velocity);
        }   
        

    }


    IEnumerator UpdateBody()
    {
        while (true)
        {
            //UpdateLeg();
            
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

    public void UpdateLeg(Vector3 _vel)
    {
        
        _vel.y = 0;
        
        _vel = Vector3.ClampMagnitude(_vel, 1);
      
        if (Physics.Raycast(transform.position, Vector3.down + _vel * forwardDistance, out hit, rayDistance))
        {
            foot.position = hit.point + GetNoise(0) * _vel.magnitude;
            foot.transform.up = hit.normal;
            foot.transform.eulerAngles = new Vector3(foot.transform.eulerAngles.x, transform.eulerAngles.y - 90 , foot.transform.eulerAngles.z);

            if (foot.parent != null)
            {
                foot.parent = null;
                //knee.parent = null;
            }
        }
        
        UpdateKnee(_vel);
        
    }

    void UpdateKnee(Vector3 _vel)
    {
        _vel = Vector3.ClampMagnitude(_vel, 1);
        
        _vel.y = 0;
        knee.position = (hip.position + foot.position) / 2 + GetNoise() + _vel * kneeVelInfluence;
    }

    Vector3 GetNoise(float _y = 1)
    {
        return new Vector3(Random.Range(-kneeNoise, kneeNoise), Random.Range(-kneeNoise, kneeNoise) * _y,
            Random.Range(-kneeNoise, kneeNoise));
    }
}
