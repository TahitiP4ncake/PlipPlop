using UnityEngine;

public interface IAbsorbable
{
    void Absorb(Absorber a);
    void Release(Absorber a);
    void Update();
    GameObject GetGameObject();
    Transform GetTransform();
    Collider GetCollider();
    MeshFilter GetMeshFilter();
    MeshRenderer GetMeshRenderer();
    Rigidbody GetRigidbody();
    Absorber GetAbsorber();
    float GetVerticalSize();
    void SetHeightInBody(float currentHeight);
}