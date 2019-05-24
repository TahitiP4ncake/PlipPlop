using UnityEngine;

public class Vehicule : MonoBehaviour
{
    public virtual void Operate(Pilote pilote){}
    public virtual void On(){}
    public virtual void Off(){}
}
