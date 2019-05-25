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

    NavMeshAgent navMeshAgent;
    GameObject visual;
    int currentLine = -1;

    private void Awake()
    {
        if (sheet == null) throw new MissingComponentException("Inhabitant has no sheet : " + name);

        navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        navMeshAgent.areaMask = sheet.navMeshAreaMask;
        navMeshAgent.speed = sheet.walkingSpeed;
        navMeshAgent.angularSpeed = 720f;
        navMeshAgent.acceleration = 50f;
        visual = Instantiate(sheet.appearance.FBXPrefab, transform);
        visual.transform.localPosition = sheet.appearance.position;
        visual.transform.localEulerAngles = sheet.appearance.eulers;
        visual.transform.localScale = sheet.appearance.scale;

        HideEmotions();
    }

    // Update is called once per frame
    void Update()
    {
        /// debug
        if (Input.GetMouseButtonDown(0)) {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                navMeshAgent.SetDestination(hit.point);
            }

        }
    }

    public void StartDialogue()
    {
        currentLine = 0;
        DisplayText(sheet.dialogSequence[currentLine]);
    }

    public void NextSentence()
    {
        currentLine = (currentLine+1)%sheet.dialogSequence.Count;
        DisplayText(sheet.dialogSequence[currentLine]);
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


}
