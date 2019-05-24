using UnityEngine;

public enum MeshPivot{ Down, Center }

public class Absorbable : MonoBehaviour, IAbsorbable
{
    [Header("Settings")]
    public MeshPivot pivot = MeshPivot.Center;

    // Referencies
    Collider collider;
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    Rigidbody rigidbody;
    Absorber absorber;

    void Awake()
    {
        collider = GetComponent<Collider>();
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        rigidbody = GetComponent<Rigidbody>();

    }

    void Start()
    {
        collider.material = Library.instance.defaultPhysicMaterial;
    }

    public virtual void Absorb(Absorber a) // Get Absorbed by the given absorber 
    {
        gameObject.layer = 14; // Ignore Player collision
        if(rigidbody != null) rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        absorber = a;
    }

    public virtual void Release(Absorber a) // Get release free by the given absorber 
    {
        transform.SetParent(null);
        gameObject.layer = 0;
        if(rigidbody != null) rigidbody.constraints = RigidbodyConstraints.None;
        absorber = null;
    }

    public virtual void Update() // Update 
    {

    }

    public virtual float GetVerticalSize() // Return the vertical height depending on the bounds and rotation
    {
        Vector3 boundsSize = Vector3.Scale(this.GetMeshFilter().mesh.bounds.size, GetTransform().localScale);
        return Vector3.Scale(boundsSize, GetTransform().up).magnitude;
    }

    public virtual float GetHorizontalSize() // Return the vertical height depending on the bounds and rotation
    {
        Vector3 boundsSize = Vector3.Scale(this.GetMeshFilter().mesh.bounds.size, GetTransform().localScale);
        return Vector3.Scale(boundsSize, GetTransform().right).magnitude;
    }

    public void SetHeightInBody(float currentHeight)
    {
        switch(pivot)
        {
            case MeshPivot.Center: 
                transform.localPosition = new Vector3(
                    transform.localPosition.x,
                    currentHeight + GetVerticalSize()/2,
                    transform.localPosition.z
                );
            break;
            case MeshPivot.Down: 
                transform.localPosition = new Vector3(
                    transform.localPosition.x,
                    currentHeight,
                    transform.localPosition.z
                );
                transform.localPosition += Vector3.Scale(-transform.up, new Vector3(0f,GetVerticalSize(),0f));
            break;
        }
    }

    // Get things
    public GameObject GetGameObject(){return gameObject;}
    public Transform GetTransform(){return transform;}
    public Collider GetCollider(){return collider;}
    public MeshFilter GetMeshFilter(){return meshFilter;}
    public MeshRenderer GetMeshRenderer(){return meshRenderer;}
    public Rigidbody GetRigidbody(){return rigidbody;}
    public Absorber GetAbsorber(){return absorber;}
}
