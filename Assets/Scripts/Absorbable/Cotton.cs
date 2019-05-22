using UnityEngine;

public class Cotton : Absorbable
{
    [Header("Settings")]
    public float appliedGravityStrength = 0.1f;
    public float appliedMaxFallSpeed = 1f;
    private float previousGravityStrength;
    private float previousMaxFallSpeed;
    private Gravity gravity;

    public override void Absorb(Absorber a)
    {
        base.Absorb(a);
        
        // Make the gravity lighter
        gravity = a.gameObject.GetComponent<Gravity>();
        if(gravity != null)
        {
            previousGravityStrength = gravity.strength;
            previousMaxFallSpeed = gravity.maxFallSpeed;
            gravity.strength = appliedGravityStrength;
            gravity.maxFallSpeed = appliedMaxFallSpeed;
        }
    }

    public override void Release(Absorber a)
    {
        base.Release(a);
        
        // Reset the gravity to its values
        if(gravity != null)
        {
            gravity.strength = previousGravityStrength;
            gravity.maxFallSpeed = previousMaxFallSpeed;
            gravity = null;
        }
    }
}
