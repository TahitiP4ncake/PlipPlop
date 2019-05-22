using UnityEngine;

public class Absorbable : MonoBehaviour, IAbsorbable
{
    // Referencies
    Collider collider;
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    Rigidbody rigidbody;

    void Awake()
    {
        collider = GetComponent<Collider>();
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        rigidbody = GetComponent<Rigidbody>();
    }

    public virtual void Absorb()
    {
        gameObject.layer = 14; // Ignore Player collision
        if(rigidbody != null) rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    public virtual  void Release()
    {
        transform.SetParent(null);
        gameObject.layer = 0;
        if(rigidbody != null) rigidbody.constraints = RigidbodyConstraints.None;
    }
    
    public virtual  void Update()
    {

    }

    public virtual float GetVerticalSize()
    {
        Vector3 boundsSize = Vector3.Scale(this.GetMeshFilter().mesh.bounds.size, GetTransform().localScale);
        return Vector3.Scale(boundsSize, GetTransform().up).magnitude;
    }

    // Get things
    public GameObject GetGameObject(){return gameObject;}
    public Transform GetTransform(){return transform;}
    public Collider GetCollider(){return collider;}
    public MeshFilter GetMeshFilter(){return meshFilter;}
    public MeshRenderer GetMeshRenderer(){return meshRenderer;}
    public Rigidbody GetRigidbody(){return rigidbody;}
}
