using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Habitant : MonoBehaviour
{

    public bool hasSomethingToSay;
    
    
    
    
    public bool canInteract = false;

    public string[] dialogues;

    public int dialogueIndex;

    public string[] screams;

    private bool screaming;
    
    //talking

    public bool talking;

    private PlayerMovement player;

    public string[] answers;

    public int answerIndex;

    private bool changedAnswer;

    public bool responded;

    void Update()
    {
        if (canInteract)
        {
            if (Input.GetButtonDown("GamepadInterract"))
            {
                if (hasSomethingToSay == false)
                {
                    Interract();
                }
                else
                {
                    if (talking)
                    {
                        if (responded)
                        {
                            Close();
                        }
                        else
                        {

                            Respond();
                        }
                    }
                    else
                    {
                        Interract();
                        talking = true;
                        player.StartTalking(transform);

                    }
                }

            }

            if (!talking)
            {
                return;
            }

            if (Input.GetAxis("Horizontal") != 0 )
            {
                if (changedAnswer == false)
                {
                    changedAnswer = true;
                    NextAnswer();
                }
            }
            else
            {
                changedAnswer = false;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {


        if (other.CompareTag("Flash") )
        {
            if (!screaming)
            {
                Scream();
            }

            return;
        }
        
        
        player = other.gameObject.GetComponentInParent<PlayerMovement>();
        if (player)
        {
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        //print("OUT");
        player = other.gameObject.GetComponentInParent<PlayerMovement>();
        if (player)
        {
            canInteract = false;
            player = null;
        }
    }
    
    void Interract()
    {
        Manager.SINGLETON.SubtitleOn(dialogues[dialogueIndex], !hasSomethingToSay);
        dialogueIndex++;

        if (dialogueIndex >= dialogues.Length)
        {
            dialogueIndex = 0;
        }

        if (hasSomethingToSay)
        {
            Manager.SINGLETON.AnswerOn();
        }
    }

    void Respond()
    {
        Manager.SINGLETON.AnswerOff();
        Manager.SINGLETON.SubtitleOn(answers[answerIndex], !hasSomethingToSay);

        responded = true;
    }

    void NextAnswer()
    {
        if (answerIndex == 0)
        {
            Manager.SINGLETON.answerText.text = "- plop -";
    
            answerIndex++;
        }
        else
        {
            answerIndex = 0;
            Manager.SINGLETON.answerText.text = "- plip -";

        }
    }

    void Close()
    {
        responded = false;
        talking = false;
        answerIndex = 0;
        player.StopTalking();
        Manager.SINGLETON.EndConversation();
    }
    
    

    void Scream()
    {
        print("scream");
        Manager.SINGLETON.SubtitleOn(screams[Random.Range(0,screams.Length)], true);
        screaming = true;
        
        Invoke("ScreamOff", Time.fixedDeltaTime);
    }

    void ScreamOff()
    {
        screaming = false;
    }
}



