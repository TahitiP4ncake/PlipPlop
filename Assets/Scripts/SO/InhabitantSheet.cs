using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Inhabitants/Sheet")]
public class InhabitantSheet : ScriptableObject
{
    public GameObject visual;
    public Inhabitant.TEAM team;
    public float walkingSpeed = 4f;
    public float runningSpeed = 6f;
    public int navMeshAreaMask;
    public List<Inhabitant.Line> dialogSequence;
    public List<Inhabitant.Line> screams;
}
