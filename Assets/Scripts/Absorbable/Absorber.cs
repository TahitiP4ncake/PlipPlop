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

        transform.position -= new Vector3(0f, a.GetVerticalSize(), 0f);

        a.Absorb();
        RoundAngles(a.GetTransform());
        a.GetTransform().SetParent(transform);
        absorbed.Add(a);

        RefreshBody();
    }

    public void Release(int index)
    {
        if(index < 0 && index >= absorbed.Count) return;

        absorbed[index].Release();
        transform.position += new Vector3(0f, absorbed[index].GetVerticalSize(), 0f);
        absorbed[index].GetTransform().position = transform.position;
        absorbed.RemoveAt(index);

        RefreshBody();
    }

    public void RoundAngles(Transform t)
    {
        Vector3 rot = new Vector3(t.eulerAngles.x, t.eulerAngles.y, t.eulerAngles.z);
        rot.x = CartesianRound(rot.x);
        rot.y = CartesianRound(rot.y);
        rot.z = CartesianRound(rot.z);
        t.rotation = Quaternion.Euler(rot);
    }

    public float CartesianRound(float value)
    {
        if(value > 0f && value <= 45f) return 0f;
        else if(value > 45f && value <= 135f) return 90f;
        else if(value > 135f && value <= 225f) return 180f;
        else if(value > 225f && value <= 315f) return 270f;
        else if(value > 315f && value <= 360f) return 0f;
        else return 0f;
    }

    void RefreshBody()
    {
        float totalHeight = hipsHeight;
        for(int i = absorbed.Count - 1; i >= 0; i--)
        {
            float myHeight = absorbed[i].GetVerticalSize();
            absorbed[i].GetTransform().localPosition = new Vector3(0f, totalHeight + myHeight/2, 0f);
            totalHeight += myHeight;
        }
        head.localPosition = new Vector3(0f, totalHeight, 0f);
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
            height += a.GetVerticalSize();
        }
        return height;
    }
}
