using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UserInterface : MonoBehaviour
{
    public TextMeshProUGUI answerPreviewText;


    void Update()
    {
        if (Game.player.IsAnswering()) {
            SetAnswerPreviewText(Game.player.GetCurrentAnswer());
        }
        else {
            EmptyAnswerPreviewText();
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
}
