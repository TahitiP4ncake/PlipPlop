using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NPCPointOfInterest : MonoBehaviour
{
    public List<Inhabitant.TEAM> concernedTeams;
    public float amusement = 10f;
    public bool isDivertingToLookAt = false;
    public bool isDivertingToStayAround = true;
    public float divertingRadius = 5f;

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = new Color(1f, 0f, 1f, 1f);
        //Gizmos.DrawWireSphere(transform.position, divertingRange);
        Handles.color = Gizmos.color;
        Handles.DrawWireDisc(transform.position, Vector3.up, divertingRadius);
    }
}
