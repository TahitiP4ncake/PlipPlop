using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Inhabitants/Sheet")]
public class InhabitantSheet : ScriptableObject
{
    [System.Serializable]
    public class Appearance
    {
        public GameObject FBXPrefab;
        public Vector3 position;
        public Vector3 eulers;
        public Vector3 scale;
    }

    public Inhabitant.TEAM team;
    public float walkingSpeed = 4f;
    public float runningSpeed = 6f;
    public Appearance appearance;
    public int navMeshAreaMask;
    public List<Inhabitant.Line> dialogSequence;
    public List<Inhabitant.Line> screams;
}
