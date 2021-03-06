﻿using UnityEngine;

public class Lead : Absorbable
{
    [Header("Settings")]
    public float appliedGravityStrength = 1f;
    private float previousGravityStrength;
    
    private Gravity gravity;
    
    public override void Absorb(Absorber a)
    {
        base.Absorb(a);
        // Making the player heavier
        gravity = a.gameObject.GetComponent<Gravity>();
        if(gravity != null)
        {
            previousGravityStrength = gravity.strength;
            gravity.strength = appliedGravityStrength;
        }
    }

    public override void Release(Absorber a)
    {
        base.Release(a);
        // Make the player lighter
        if(gravity != null) gravity.strength = previousGravityStrength;
    }

    public override void Update()
    {
        base.Update();

        // Smash the detected Smashable if the rb is going fast enough
        if(GetAbsorber() != null && gravity != null && gravity.GetVelocity().y < 0f)
        {
            RaycastHit hit;
            if(Physics.Raycast(GetAbsorber().transform.position, -transform.up, out hit, 1f))
            {
                ISmashable s = hit.transform.gameObject.GetComponent<ISmashable>();
                if(s != null) s.Smash();
            }
        }
    }
}
