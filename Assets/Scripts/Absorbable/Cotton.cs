using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cotton : Absorbable
{
    public float appliedGravityStrength = 0.1f;
    public float appliedMaxFallSpeed = 1f;
    float previousGravityStrength;
    float previousMaxFallSpeed;

    public override void Absorb(Absorber a)
    {
        base.Absorb(a);
        
        Gravity g = a.gameObject.GetComponent<Gravity>();

        if(g != null)
        {
            previousGravityStrength = g.strength;
            previousMaxFallSpeed = g.maxFallSpeed;

            g.strength = appliedGravityStrength;
            g.maxFallSpeed = appliedMaxFallSpeed;
        }
    }

    public override void Release(Absorber a)
    {
        base.Release(a);
        
        Gravity g = a.gameObject.GetComponent<Gravity>();
        
        if(g != null)
        {
            g.strength = previousGravityStrength;
            g.maxFallSpeed = previousMaxFallSpeed;
        }
    }
}
