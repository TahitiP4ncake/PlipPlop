using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteCube : Absorbable
{
    public Transform toDisplay;
    public AnimationCurve curve;
    private bool visible;

    public override void Absorb(Absorber a)
    {
        base.Absorb(a);
        StartCoroutine(ShowSprite());
    }

    IEnumerator ShowSprite()
    {
        visible = true;
        float _y = 0;
        while (_y < 1)
        {
            toDisplay.localScale = Vector3.Lerp(Vector3.zero, Vector3.one,curve.Evaluate(_y));
            _y += Time.deltaTime*3;
            yield return null;
        }
        
    }
}
