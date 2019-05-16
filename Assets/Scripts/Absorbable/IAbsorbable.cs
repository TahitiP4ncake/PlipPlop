using UnityEngine;

public interface IAbsorbable
{
    void Absorb();
    void Release();
    void Update();

    GameObject GetGameObject();
    Transform GetTransform();
    Collider GetCollider();
    MeshFilter GetMeshFilter();
    MeshRenderer GetMeshRenderer();
    Rigidbody GetRigidbody();
}