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
        public TextMeshPro textMesh;
    }

    public TEAM team;
    public InhabitantSheet sheet;
    public EmotionDisplayers emotionDisplayers;
    public float textFadeSpeed = 60f;
    public float changeFunStanceMaxEvery = 5f;

    NavMeshAgent navMeshAgent;
    GameObject visual;
    BodyAnimations bodyAnimations;
    int currentLine = -1;

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

        bodyAnimations = GetComponentInChildren<BodyAnimations>();

        navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        navMeshAgent.areaMask = sheet.navMeshAreaMask;
        navMeshAgent.speed = sheet.walkingSpeed;
        navMeshAgent.angularSpeed = 720f;
        navMeshAgent.acceleration = 50f;
        visual = Instantiate(sheet.appearance.FBXPrefab, transform);
        visual.transform.localPosition = sheet.appearance.position;
        visual.transform.localEulerAngles = sheet.appearance.eulers;
        visual.transform.localScale = sheet.appearance.scale;
        foreach(var renderer in visual.GetComponentsInChildren<Renderer>()) {
            renderer.material = sheet.appearance.material;
        }

        HideEmotions();

        UpdatePointsOfInterest();
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
            var cut = Random.value * changeFunStanceMaxEvery;
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

    Vector3 GetRandomPointAroundTargetPOI(float range = 1f)
    {
        var pos = pointsOfInterests[targetPOI].transform.position;
        return new Vector3(
            pos.x - range / 2 + Random.value * range,
            pos.y,
            pos.z - range / 2 + Random.value * range
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
        DisplayText(sheet.dialogSequence[currentLine]);
        return GetPossibleAnswers().Count > 0;
    }

    public void NextSentence()
    {
        currentLine = sheet.dialogSequence[currentLine].nextLine;
        if (currentLine < 0) {
            currentLine = 0;
            EndDiscussion();
            return;
        }
        DisplayText(sheet.dialogSequence[currentLine]);
    }

    public void SetSentence(int index)
    {
        currentLine = index;
        DisplayText(sheet.dialogSequence[currentLine]);
    }

    public List<Line.Answer> GetPossibleAnswers()
    {
        return sheet.dialogSequence[currentLine].answers;
    }

    public void EndDiscussion()
    {
        StartCoroutine(FadeTextOut(delegate { }));
    }

    void DisplayText(Line line)
    {
        StartCoroutine(FadeTextOut(
            delegate {
                emotionDisplayers.textMesh.enabled = true;
                emotionDisplayers.textMesh.text = line.text;
                emotionDisplayers.textMesh.alpha = 0f;
                StartCoroutine(FadeTextIn());
            }
        ));
    }

    IEnumerator FadeTextOut(System.Action callback)
    {
        if (!emotionDisplayers.textMesh.enabled) {
            callback.Invoke();
            yield break;
        }
        while (emotionDisplayers.textMesh.alpha > 0f) {
            emotionDisplayers.textMesh.alpha -= (textFadeSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        callback.Invoke();
        yield return true;
    }

    IEnumerator FadeTextIn()
    {   
        while(emotionDisplayers.textMesh.alpha < 1f) {
            emotionDisplayers.textMesh.alpha += (textFadeSpeed*Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        yield return true;
    }

    void HideEmotions()
    { 
        emotionDisplayers.exclamationMark.SetActive(false);
        emotionDisplayers.questionMark.SetActive(false);
        emotionDisplayers.textMesh.enabled = false;
    }

    public void Scream()
    {
        if (sheet.screams.Count < 0) return;
        DisplayText(sheet.screams[Random.Range(0, sheet.screams.Count)]);
    }

}
