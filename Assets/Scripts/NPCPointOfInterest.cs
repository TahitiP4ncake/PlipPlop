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
    [Range(0f,1f)]public float divertingSafeZone = 0.3f;

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        //Gizmos.DrawWireSphere(transform.position, divertingRange);
        Handles.color = new Color(1f, 0f, 1f, 1f);
        Handles.DrawWireDisc(transform.position, Vector3.up, divertingRadius);
        Handles.color = new Color(0f, 1f, 1f, 1f);
        Handles.DrawWireDisc(transform.position, Vector3.up, divertingRadius*divertingSafeZone);
    }
}
