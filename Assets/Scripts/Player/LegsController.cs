using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegsController : MonoBehaviour
{
    public Animator anim;
    public ParticleSystem[] footDust;
    public ParticleSystem landDust;

    bool isGrounded = false;

    public void SetGrounded(bool ground)
    {
        anim.SetBool("IsGrounded", ground);
        isGrounded = ground;
    }

    public void SetSpeed(float value)
    {
        value = Mathf.Clamp(value, 0f, 1f);
        anim.SetFloat("Speed", value);
    }

    public void Jump()
    {
        anim.SetTrigger("JumpAction");
        landDust.Play();
        //PlaySound
    }

    public void Step(int feetIndex)
    {
        footDust[feetIndex].Play();
        //PlaySound
    }
}
