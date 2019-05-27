using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece
{
    public Transform t;
    public Rigidbody rb;
    public Collider c;
}

public class Breaking : MonoBehaviour, ISmashable
{
    public Collider originCollider;
    List<Piece> pieces = new List<Piece>();

    public void Start()
    {
        Load();
    }

    public void Load()
    {
        Transform[] ts = gameObject.GetComponentsInChildren<Transform>();

        foreach(Transform t in ts)
        {
            Rigidbody rb = t.GetComponent<Rigidbody>();
            Collider c = t.GetComponent<Collider>();

            if(rb != null && c != null)
            {
                Piece p = new Piece();
                p.rb = rb;
                p.c = c;
                p.t = t;
                pieces.Add(p);
            }
        }
        foreach(Piece p in pieces) 
        {
            p.rb.useGravity = false;
            p.c.enabled = false;
        }
        originCollider.enabled = true;
    }

    [ContextMenu("ofheofzef")]
    public void Break()
    {
        foreach(Piece p in pieces) 
        {
            p.rb.useGravity = true;
            p.rb.AddForce(
                new Vector3(
                    Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f),
                    Random.Range(-1f, 1f)
                ) * 10f,
                ForceMode.Impulse
            );
            p.c.enabled = true;
            p.t.SetParent(null);
        }
        Destroy(gameObject);
    }

    public void Smash()
    {
        Break();
    }
}
