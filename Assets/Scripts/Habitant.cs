using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habitant : MonoBehaviour
{
    public bool canInteract = false;

    public string[] dialogues;

    public int dialogueIndex;

    void Update()
    {
        if (Input.GetButtonDown("GamepadInterract"))
        {
            if (canInteract)
            {
                Interract();
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
print("IN");
        PlayerMovement _player = other.gameObject.GetComponentInParent<PlayerMovement>();
        if (_player)
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        print("OUT");
        PlayerMovement _player = other.gameObject.GetComponentInParent<PlayerMovement>();
        if (_player)
        {
            canInteract = false;
        }
    }
    
    void Interract()
    {
        Manager.SINGLETON.SubtitleOn(dialogues[dialogueIndex]);
        dialogueIndex++;

        if (dialogueIndex >= dialogues.Length)
        {
            dialogueIndex = 0;
        }
    }
}



