using UnityEngine;

public class Vehicule : MonoBehaviour
{
    public virtual void Operate(Pilot pilot){}
    public virtual void On(){}
    public virtual void Off(){}
}
