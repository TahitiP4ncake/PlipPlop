using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public bool currentGround;

    public Transform respawnTransform;

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Player") && !currentGround)
        {
            //currentGround = true;

            //other.collider.gameObject.GetComponentInParent<PlayerMovement>().ChangeGround(this);
        }
    }
}
