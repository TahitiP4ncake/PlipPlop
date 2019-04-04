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

    private Coroutine scaleCor;


    public AnimationCurve[] curves;
    
    
    //Points

    public Animator points;

    void Update()
    {
        
        
        
        if (canInteract)
        {
 
            //Rotate the point toward the player
            Vector3 relativePos = player.transform.position - transform.position;
            relativePos.y = 0;
            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

            points.transform.rotation = Quaternion.Slerp(points.transform.rotation, rotation, Time.deltaTime * 2);


            if (player.IsGround)
            {
                return;
            }
            
            
            if (Input.GetButtonDown("GamepadInterract"))
            {
                if (hasSomethingToSay == false)
                {
                    Interract();
               
                   StartTalking(player.transform);
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
                            if (scaleCor != null)
                            {
                                StopCoroutine(scaleCor);
                            }

                            scaleCor = StartCoroutine(Animate(1));
                            
                            Respond();
                        }
                    }
                    else
                    {
                        Interract();
                        talking = true;
                        
                        if(player!=null)
                        player.StartTalking(transform);
                        
                        StartTalking(player.transform);
                    }
                }

            }

            if (!talking || responded)
            {
                return;
            }

            float _h = Input.GetAxis("Horizontal");
            
            if (_h >float.Epsilon )
            {
                if (changedAnswer == false)
                {
                    changedAnswer = true;
                    NextAnswer(1);
                }
            }
            else if (_h < -float.Epsilon)
            {
                if (changedAnswer == false)
                {
                    changedAnswer = true;
                    NextAnswer(0);
                }
            }
            else
            {
                changedAnswer = false;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Manager.SINGLETON.PlaySound("bump",.7f);


        if (scaleCor != null)
        {
            StopCoroutine(scaleCor);
        }
        
            scaleCor = StartCoroutine(Animate(0));
        
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

            if (!canInteract)
            {
                canInteract = true;
                points.SetBool("Question", true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        points.SetBool("Question", false);
        //print("OUT");
        //TEMP
        //player = other.gameObject.GetComponentInParent<PlayerMovement>();
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
        
        
        if (scaleCor != null)
        {
            StopCoroutine(scaleCor);
        }

        scaleCor = StartCoroutine(Animate(1));
    }

    void Respond()
    {
        Manager.SINGLETON.AnswerOff();
        Manager.SINGLETON.SubtitleOn(answers[answerIndex], !hasSomethingToSay);

        responded = true;
    }

    void NextAnswer(int _value)
    {
        
        Manager.SINGLETON.ChangeAnswer(_value);

        answerIndex = _value;
    }

    void Close(bool _scream = false)
    {
        responded = false;
        talking = false;
        answerIndex = 0;
        if (player != null)
        {
            player.StopTalking();
        }

        Manager.SINGLETON.EndConversation(_scream);
        points.SetBool("Question", false);

    }
    
    

    void Scream()
    {
        points.SetTrigger("Surprise");
        
        
        print("scream");
        Manager.SINGLETON.SubtitleOn(screams[Random.Range(0,screams.Length)], true);
        screaming = true;
        
        Invoke("ScreamOff", Time.fixedDeltaTime);
        
        Close(true);
        
        if (scaleCor != null)
        {
            StopCoroutine(scaleCor);
        }

        scaleCor = StartCoroutine(Animate(1));
    }

    void ScreamOff()
    {
        screaming = false;
    }

    void StartTalking(Transform _player)
    {
        StartCoroutine(LookAtPlayer(_player));
    }

    IEnumerator LookAtPlayer(Transform _player)
    {
        float _y = 0;
        
        Vector3 relativePos = _player.position - transform.position;
        relativePos.y = 0;

        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);

        while (_y < 1)
        {

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, _y);
            _y += Time.deltaTime*3;
            yield return null;
        }

    }

    IEnumerator Animate(int _index)
    {
        float _y = 0;
        
        while (_y < 1)
        {
            transform.localScale = Vector3.one * curves[_index].Evaluate(_y);
            
            _y += Time.deltaTime;
            yield return null;
        }
        
        transform.localScale = Vector3.one;


        scaleCor = null;
    }


    
    
}



