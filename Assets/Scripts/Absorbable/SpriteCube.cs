using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteCube : Absorbable
{
    public Transform toDisplay;

    public override void Absorb(Absorber a)
    {
        base.Absorb(a);
        
    }   

    public override void Release(Absorber a)
    {
        base.Release(a);

    }
}
