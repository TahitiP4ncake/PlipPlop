using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Absorber : MonoBehaviour
{
    public Transform head;
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

    public void Absorb(IAbsorbable a)
    {
        a.Absorb();
        a.GetTransform().SetParent(transform);
        a.GetTransform().localPosition = new Vector3(0f, GetBodyHeight() + a.GetMeshFilter().mesh.bounds.size.y/2, 0f);
        absorbed.Add(a);
        RefreshHead();
    }

    public void Release(int index)
    {
        if(index < 0 && index >= absorbed.Count) return;
        absorbed[index].Release();
        absorbed.RemoveAt(index);
        RefreshHead();
    }

    public void ReleaseLast()
    {
        if(absorbed.Count > 0) Release(absorbed.Count - 1);
    }

    public void ReleaseAll()
    {
        foreach(IAbsorbable a in absorbed) a.Release();
        absorbed.Clear();
    }

    public float GetBodyHeight()
    {
        float height = hipsHeight;
        foreach(IAbsorbable a in absorbed) 
        {
            height += a.GetMeshFilter().mesh.bounds.size.y * a.GetTransform().localScale.y;
        }
        return height;
    }

    public void RefreshHead()
    {
        head.localPosition = new Vector3(0f, GetBodyHeight(), 0f);
    }
}
