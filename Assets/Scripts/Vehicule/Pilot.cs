using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pilot : MonoBehaviour
{
    public PlayerController controller;
    public PlayerInputs inputs;
    private Vehicule vehicule;
    public float range;

    public void TryTakeControl()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position + new Vector3(0f, 0.1f, 0f), -transform.up, out hit, range - 0.1f))
        {
            Vehicule v = hit.transform.gameObject.GetComponent<Vehicule>();
            if(v != null) TakeControl(v);
        }
    }

    public void TryLooseControl()
    {
        if(vehicule != null) LooseControl();
    }

    public void Update()
    {
        if(vehicule != null) vehicule.Operate(this);
    }

    public void TakeControl(Vehicule v)
    {
        controller.StopMovement();
        controller.StopPhysics();
        transform.SetParent(v.transform);

        vehicule = v;
        vehicule.On();
    }

    public void LooseControl()
    {
        controller.ActivateMovement();
        controller.ActivePhysics();
        transform.SetParent(null);

        vehicule.Off();
        vehicule = null;
    }
}
