using UnityEngine;

public class Absorbable : MonoBehaviour, IAbsorbable
{
    // Referencies
    Collider collider;
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    Rigidbody rigidbody;


    public virtual void Absorb()
    {
         
    }

    public virtual  void Release()
    {

    }
    
    public virtual  void Update()
    {

    }

    // Get things
    public GameObject GetGameObject(){return gameObject;}
    public Transform GetTransform(){return transform;}
    public Collider GetCollider(){return collider;}
    public MeshFilter GetMeshFilter(){return meshFilter;}
    public MeshRenderer GetMeshRenderer(){return meshRenderer;}
    public Rigidbody GetRigidbody(){return rigidbody;}
}
