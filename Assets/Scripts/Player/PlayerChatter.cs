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
        };
    }

    void ResetInterlocutor()
    {
        currentInterlocutor = null;
        currentAnswerIndex = 0;
        isWaitingForAnswer = false;
    }

    public void TryTalk()
    {
        if (currentInterlocutor != null && IsAtRange(currentInterlocutor)) {
            currentInterlocutor.NextSentence();
        }
        else {
            ResetInterlocutor();
            if (IsAnyInhabitantAtRange()) {
                currentInterlocutor = GetNearestInhabitantAtTalkingRange();
                currentInterlocutor.StartDialogue();
            }
        }
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

}
