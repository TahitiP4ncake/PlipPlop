using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class Inhabitant : MonoBehaviour
{
    public enum TEAM { TOURIST, INHABITANT, STAFF, ANIMAL };

    [System.Serializable]
    public class Line
    {
        [System.Serializable]
        public class Answer
        {
            public int nextLineId;
            public string text;
        }
        public List<Answer> answers;
        public string text = "ploupbidouwa";
        public int nextLine;
    }

    [System.Serializable]
    public class EmotionDisplayers
    {
        public GameObject exclamationMark;
        public GameObject questionMark;
        public GameObject dialogueAnchor;
    }

    public TEAM team;
    public InhabitantSheet sheet;
    public EmotionDisplayers emotionDisplayers;
    public float textFadeSpeed = 60f;
    public float changeFunStanceMaxEvery = 5f;
    public CollisionEventTransmitter chatterCollider;
    [Range(0f,1f)]public float stanceChangeMinimumProportion = 0.75f;
    public float chatterCooldownSeconds = 2f;
    public float conversationLength = 4f;
    [Range(0f, 1f)] public float conversationLengthMinimumProportion = 0.4f;

    bool isTalking = false;
    NavMeshAgent navMeshAgent;
    GameObject visual;
    BodyAnimations bodyAnimations;
    int currentLine = -1;
    float lastTalkTime;

    #region AI

    float currentAmusement = 0f;
    bool isWalkingTowardsTarget = false;
    int targetPOI = 0;
    bool isHavingFun = false;
    List<NPCPointOfInterest> pointsOfInterests = new List<NPCPointOfInterest>();

    #endregion

    private void Awake()
    {
        if (sheet == null) throw new MissingComponentException("Inhabitant has no sheet : " + name);

        navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        navMeshAgent.areaMask = sheet.navMeshAreaMask;
        navMeshAgent.speed = sheet.walkingSpeed;
        navMeshAgent.angularSpeed = 720f;
        navMeshAgent.acceleration = 50f;
        visual = Instantiate(sheet.visual, transform);
        bodyAnimations = GetComponentInChildren<BodyAnimations>();
        bodyAnimations.body = visual.transform;
        navMeshAgent.radius = 1.5f;

        chatterCollider.onTriggerEnter += (x) => {
            var interlocutor = x.GetComponent<Inhabitant>();
            if (interlocutor != null && interlocutor!= this) {
                if (!interlocutor.IsInChatterCooldown() && !interlocutor.IsTalking() && !IsInChatterCooldown() && !IsTalking()) {
                    float duration = conversationLength * conversationLengthMinimumProportion + Random.value * (1 - conversationLengthMinimumProportion) * conversationLength;
                    StartDiscussionWith(interlocutor, duration);
                    interlocutor.StartDiscussionWith(this, duration);
                }
            }
        };

        HideEmotions();

        UpdatePointsOfInterest();
    }

    public void StartDiscussionWith(Inhabitant other, float duration)
    {
        isTalking = true;
        bodyAnimations.StartTalking();
        PauseWalking();
        transform.LookAt(
            new Vector3(
                other.transform.position.x,
                transform.position.y,
                other.transform.position.z
            )
        );
        StartCoroutine(ExecuteIn(
                delegate {
                    EndDiscussion();
                    bodyAnimations.EndTalking();
                },
                duration
            )
        );
    }

    IEnumerator ExecuteIn(System.Action callback, float delay = 1f)
    {
        yield return new WaitForSeconds(delay);
        callback.Invoke();
    }

    public bool IsTalking()
    {
        return isTalking;
    }

    public bool IsInChatterCooldown()
    {
        return lastTalkTime + chatterCooldownSeconds > System.DateTime.Now.Second;
    }

    void UpdatePointsOfInterest()
    {
        pointsOfInterests.Clear();
        var potentialPOIs = FindObjectsOfType<NPCPointOfInterest>();
        foreach (var POI in potentialPOIs) {
            if (navMeshAgent.CalculatePath(POI.transform.position, new NavMeshPath())) {
                pointsOfInterests.Add(POI);
            }
        }
        pointsOfInterests.RemoveAll(o => !o.concernedTeams.Contains(team));
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHavingFun) {
            if (!isWalkingTowardsTarget) { 
                FindNewDiversion();
                WalkTowardsTargetPOI();
            }

            if (HasReachedTarget()) {
                isWalkingTowardsTarget = false;
                HaveFun();
            }
        }

        bodyAnimations.velocity = navMeshAgent.velocity / sheet.walkingSpeed;
    }

    void HaveFun()
    {
        var POI = pointsOfInterests[targetPOI];
        isHavingFun = true;
        currentAmusement = POI.amusement / 2f + Random.value * (POI.amusement / 2f);
        StartCoroutine(ProfitOfPOI());
    }

    IEnumerator ProfitOfPOI()
    {
        var POI = pointsOfInterests[targetPOI];
        var stanceChanges = new List<float>();
        var totalAmusement = currentAmusement;
        while (totalAmusement > 0f) {
            var cut = Random.value * changeFunStanceMaxEvery * (1f-stanceChangeMinimumProportion) 
                + stanceChangeMinimumProportion * changeFunStanceMaxEvery;
            totalAmusement -= cut;
            stanceChanges.Add(totalAmusement);
        }
        while (currentAmusement > 0f) {
            currentAmusement -= Time.deltaTime;

            if (POI.isDivertingToStayAround && 
                stanceChanges.Count > 0 && currentAmusement < stanceChanges[0]) {
                stanceChanges.RemoveAt(0);
                navMeshAgent.SetDestination(
                    GetRandomPointAroundTargetPOI(POI.divertingRadius)
                );
            }

            if (POI.isDivertingToLookAt && navMeshAgent.remainingDistance == 0f) {
                transform.LookAt(new Vector3(
                    POI.transform.position.x,
                    transform.position.y,
                    POI.transform.position.z
                ));
            }

            yield return new WaitForEndOfFrame();
        }

        isHavingFun = false;
    }

    Vector3 GetRandomPointAroundTargetPOI(float range = 1f, float minRangePortion = 0.5f)
    {
        var pos = pointsOfInterests[targetPOI].transform.position;
        var minRadius = range * minRangePortion;

        var randomRadius = Random.value * (range - minRadius) + minRadius;
        var randomAngle = Mathf.Deg2Rad * Random.value * 360f;
        
        return new Vector3(
            pos.x + Mathf.Cos(randomAngle) * randomRadius,
            pos.y,
            pos.z + Mathf.Sin(randomAngle) * randomRadius
        );
    }

    bool HasReachedTarget()
    {
        var POI = pointsOfInterests[targetPOI];
        if (navMeshAgent.remainingDistance <= POI.divertingRadius) {
            return true;
        }

        return false;
    }

    void PauseWalking()
    {
        navMeshAgent.isStopped = true;
    }

    void ResumeWalking()
    {
        navMeshAgent.isStopped = false;
    }

    void WalkTowardsTargetPOI()
    {
        var POI = pointsOfInterests[targetPOI];
        navMeshAgent.SetDestination(GetRandomPointAroundTargetPOI(POI.divertingRadius));
        isWalkingTowardsTarget = true;
    }

    void FindNewDiversion()
    {
        var candidatesArray = new NPCPointOfInterest[pointsOfInterests.Count];
        pointsOfInterests.CopyTo(candidatesArray);
        var candidates = new List<NPCPointOfInterest>(candidatesArray);
        if (targetPOI < candidates.Count) candidates.RemoveAll(o => o == candidatesArray[targetPOI]);
        if (candidates.Count == 0) return;
        var candidate = candidates[Random.Range(0, candidates.Count)];

        targetPOI = pointsOfInterests.FindIndex(o=>o== candidate);

    }

    public bool StartDialogue()
    {
        currentLine = 0;
        isTalking = true;
        PauseWalking();
        transform.LookAt(
            new Vector3(
                Game.player.transform.position.x,
                transform.position.y,
                Game.player.transform.position.z
            )
        );

        return GetPossibleAnswers().Count > 0;
    }

    public bool NextSentence()
    {
        currentLine = sheet.dialogSequence[currentLine].nextLine;
        if (currentLine < 0) {
            currentLine = 0;
            EndDiscussion();
            return false;
        }
        return true;
    }

    public string GetCurrentLineText()
    {
        return sheet.dialogSequence[currentLine].text;
    }

    public void SetSentence(int index)
    {
        currentLine = index;
    }

    public List<Line.Answer> GetPossibleAnswers()
    {
        return sheet.dialogSequence[currentLine].answers;
    }

    public void EndDiscussion()
    {
        isTalking = false;
        lastTalkTime = System.DateTime.Now.Second;
        ResumeWalking();
    }

    void HideEmotions()
    { 
        emotionDisplayers.exclamationMark.SetActive(false);
        emotionDisplayers.questionMark.SetActive(false);
    }

    public void Scream()
    {
        if (sheet.screams.Count < 0) return;
        //DisplayText(sheet.screams[Random.Range(0, sheet.screams.Count)]);
    }

}
