using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Photo : MonoBehaviour
{
    public GameObject flash;

    public PlayerMovement player;
   
    
    void Update()
    {
            if (Input.GetButtonDown("GamepadInterract"))
            {
                Manager.SINGLETON.PlaySound("flash",.4f);

                flash.SetActive(true);

                flash.transform.localEulerAngles =
                    new Vector3(Random.Range(-5f, 5f), Random.Range(.5f, 5f), Random.Range(0, 361));
                flash.transform.localScale = Vector3.one * Random.Range(.9f, 1.2f);
                CancelInvoke();

                Invoke("FlashOff", .1f);

            }
       
    }

    void FlashOff()
    {
        flash.SetActive(false);
    }
}
