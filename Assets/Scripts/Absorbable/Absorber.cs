using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorber : MonoBehaviour
{
    public float range = 1f;
    public float hipsHeight = 0f;
    public List<IAbsorbable> absorbed = new List<IAbsorbable>();

    public void TryAbsorb()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position + new Vector3(0f, 0.1f, 0f), -transform.up, out hit, range - 0.1f))
        {
            IAbsorbable a = hit.transform.gameObject.GetComponent<IAbsorbable>();
            if(a != null) Absorb(a);
        }
    }

    public void Absorb(IAbsorbable element)
    {
        element.Absorb();
        absorbed.Add(element);
    }

    public void Release(int index)
    {
        if(index < 0 && index >= absorbed.Count) return;

        absorbed[index].Absorb();
        absorbed.RemoveAt(index);
    }

    public void ReleaseAll()
    {
        foreach(IAbsorbable a in absorbed) a.Release();
        absorbed.Clear();
    }

    public float GetBodyHeight()
    {
        float height = hipsHeight;
        foreach(IAbsorbable a in absorbed) hipsHeight += a.GetMeshFilter().mesh.bounds.size.y;
        return height;
    }
}
