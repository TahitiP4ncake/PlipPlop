using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserInterface : MonoBehaviour
{
    public TextMeshProUGUI answerPreviewText;
    public TextMeshProUGUI dialogueText;

    public float textFadeSpeed = 15f;

    void Update()
    {
        if (Game.player.IsAnswering()) {
            SetAnswerPreviewText(Game.player.GetCurrentAnswer());
        }
        else {
            EmptyAnswerPreviewText();
        }

        if (Game.player.IsTalking()) {
            SetDialogueText(Game.player.GetCurrentLine(), Game.player.GetInterlocutorChatterPosition());
        }
        else {
            HideDialogueText();
        }
    }

    void SetAnswerPreviewText(string answerString)
    {
        answerPreviewText.text = string.Format("< {0} >", answerString);
    }

    void EmptyAnswerPreviewText()
    {
        answerPreviewText.text = "";
    }

    void SetDialogueText(string content, Vector3 position)
    {
        dialogueText.enabled = true;
        dialogueText.fontSize = 72f;
        print(position);
        dialogueText.GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(position);
        dialogueText.text = content;
    }

    void HideDialogueText()
    {
        dialogueText.enabled = false;
    }

    void DisplayText(TextMeshProUGUI text, string content)
    {
        StartCoroutine(FadeTextOut(
            text,
            delegate {
                text.enabled = true;
                text.text = content;
                text.alpha = 0f;
                StartCoroutine(FadeTextIn(text));
            }
        ));
    }

    IEnumerator FadeTextOut(TextMeshProUGUI text, System.Action callback)
    {
        if (!text.enabled) {
            callback.Invoke();
            yield break;
        }
        while (text.alpha > 0f) {
            text.alpha -= (textFadeSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        callback.Invoke();
        yield return true;
    }

    IEnumerator FadeTextIn(TextMeshProUGUI text)
    {
        while (text.alpha < 1f) {
            text.alpha += (textFadeSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
        yield return true;
    }
}
