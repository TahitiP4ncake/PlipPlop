using UnityEngine;

public class Smashable : MonoBehaviour
{
    bool smashed;

    public void Smash()
    {
        if(!smashed) 
        {
            Debug.Log(gameObject.name + " has been smashed");
            smashed = true;
        }
    }
}
