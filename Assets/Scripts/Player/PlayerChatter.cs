using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChatter : MonoBehaviour
{
    public CollisionEventTransmitter talkableCollider;

    Inhabitant currentInterlocutor;
    int currentAnswerIndex;
    bool isWaitingForAnswer;
    List<Inhabitant> talkableInhabitants = new List<Inhabitant>();

    // Start is called before the first frame update
    void Start()
    {
        talkableCollider.onTriggerEnter += (x) => {
            var inhabitant = x.gameObject.GetComponent<Inhabitant>();
            if (inhabitant!=null) talkableInhabitants.Add(inhabitant);
        };
        talkableCollider.onTriggerExit += (x) => {
            var inhabitant = x.gameObject.GetComponent<Inhabitant>();
            if (inhabitant != null) talkableInhabitants.RemoveAll(o=>o==inhabitant);
            if (inhabitant == currentInterlocutor) ResetInterlocutor();
        };
    }

    public bool IsDiscussing()
    {
        return currentInterlocutor != null;
    }

    public string GetCurrentLine()
    {
        return currentInterlocutor.GetCurrentLineText();
    }

    void ResetInterlocutor()
    {
        if (currentInterlocutor!=null) currentInterlocutor.EndDiscussion();
        currentInterlocutor = null;
        currentAnswerIndex = 0;
        isWaitingForAnswer = false;
    }

    public void TryTalk()
    {
        if (currentInterlocutor != null && IsAtRange(currentInterlocutor)) {
            if (currentInterlocutor.GetPossibleAnswers().Count > 0) {
                PickAnswer();
            }
            else {
                bool stillTalking = currentInterlocutor.NextSentence();
                if (!stillTalking) {
                    ResetInterlocutor();
                }
                else {
                    if (currentInterlocutor.GetPossibleAnswers().Count > 0) {
                        isWaitingForAnswer = true;
                    }
                    else {
                        isWaitingForAnswer = false;
                    }
                }
            }
        }
        else {
            ResetInterlocutor();
            if (IsAnyInhabitantAtRange()) {
                currentInterlocutor = GetNearestInhabitantAtTalkingRange();
                isWaitingForAnswer = currentInterlocutor.StartDialogue();
            }
        }
    }
    
    void PickAnswer()
    {
        isWaitingForAnswer = false;
        int index = currentInterlocutor.GetPossibleAnswers()[currentAnswerIndex].nextLineId;
        currentAnswerIndex = 0;
        if (index < 0) {
            ResetInterlocutor();
            return;
        }
        currentInterlocutor.SetSentence(index);
    }

    bool IsAtRange(Inhabitant inhabitant)
    {
        return talkableInhabitants.Contains(inhabitant);
    }

    Inhabitant GetNearestInhabitantAtTalkingRange()
    {
        if (!IsAnyInhabitantAtRange()) throw new System.Exception("Tried to get nearest inhabitant but noone is at range");
        else if (talkableInhabitants.Count == 1) return talkableInhabitants[0];

        var distancePerInhabitant = new Dictionary<float, Inhabitant>();
        foreach (var inhabitant in talkableInhabitants) {
            distancePerInhabitant.Add(Vector3.Distance(transform.position, inhabitant.transform.position), inhabitant);
        }
        var keys = distancePerInhabitant.Keys.ToList();
        keys.Sort();

        return distancePerInhabitant[keys[0]];
    }


    bool IsAnyInhabitantAtRange()
    {
        return talkableInhabitants.Count > 0;
    }

    public bool IsWaitingForAnswer()
    {
        return isWaitingForAnswer;
    }

    public string GetCurrentSelectedAnswer()
    {
        return currentInterlocutor.GetPossibleAnswers()[currentAnswerIndex].text;
    }


    public void NextAnswer()
    {
        if (currentInterlocutor == null) return;
        currentAnswerIndex = (currentAnswerIndex + 1) % currentInterlocutor.GetPossibleAnswers().Count;
    }

    public void PreviousAnswer()
    {
        if (currentInterlocutor == null) return;
        currentAnswerIndex = (currentAnswerIndex - 1);
        if (currentAnswerIndex < 0) {
            currentAnswerIndex = currentInterlocutor.GetPossibleAnswers().Count - 1;
        }
    }

    public Vector3 GetInterlocutorChatterPosition()
    {
        return currentInterlocutor.emotionDisplayers.dialogueAnchor.transform.position;
    }
}
