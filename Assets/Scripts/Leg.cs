using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leg : MonoBehaviour
{
    public Transform foot;
    public Transform hip;

    private RaycastHit hit;

    public bool lerping;

    public float fps = 4;

    public float forwardDistance = 1;

    public float rayDistance = 2;
    
    private void Start()
    {
        foot.transform.parent = null;
        //StartCoroutine(UpdateBody());
    }

    private void Update()
    {
        hip.eulerAngles = foot.eulerAngles;
        //hip.LookAt(foot);
        //hip.localEulerAngles += new Vector3(90,-90,180);
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
        Vector3.ClampMagnitude(_vel, 1);
        
        _vel.y = 0;
        
        if (Physics.Raycast(transform.position, Vector3.down + _vel * forwardDistance, out hit, rayDistance))
        {
            foot.position = hit.point;
            foot.transform.up = hit.normal;
            foot.transform.eulerAngles = new Vector3(foot.transform.eulerAngles.x, transform.eulerAngles.y - 90f, foot.transform.eulerAngles.z);
        }
    }
}
