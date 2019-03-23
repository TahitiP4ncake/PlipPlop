using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Emotion
{
    Idle,
    Smile,
    Sad
}

public class Emotions : MonoBehaviour
{

    public List<GameObject> eyes;

    public SkinnedMeshRenderer mouth;

    private Emotion emotion = Emotion.Idle;

    [Space]
    
    public float shutTime;
    
    public float blinkTime;

    
    void Start()
    {
        BlinkOn();
    }

    public void ChangeEmotion(Emotion _emotion)
    {
        emotion = _emotion;

       StopAllCoroutines();

        switch (emotion)
        {
                case Emotion.Idle:
                     StartCoroutine(Idle());
                    break;
                
                case Emotion.Sad:
                     StartCoroutine(Sad());
                    break;
                   
                case Emotion.Smile:
                     StartCoroutine(Smile());
                    break;
        }
    }

    void BlinkOn()
    {
        foreach (var _eye in eyes)
        {
            _eye.SetActive(false);
        }
            
       Invoke("BlinkOff",Random.Range(shutTime/2,shutTime*2));
            
       
    }

    void BlinkOff()
    {
        foreach (var _eye in eyes)
        {
            _eye.SetActive(true);
        }
        
        Invoke("BlinkOn",Random.Range(blinkTime/2,blinkTime*2));

    }

    IEnumerator Smile()
    {
        float _y = 0;

        while (_y < 1)
        {
            mouth.SetBlendShapeWeight(0,Mathf.Lerp(mouth.GetBlendShapeWeight(0),100, _y));
            mouth.SetBlendShapeWeight(1,Mathf.Lerp(mouth.GetBlendShapeWeight(1),0, _y));

            _y += Time.deltaTime ;
            yield return null;
        }
    }
    
    IEnumerator Idle()
    {
        float _y = 0;

        while (_y < 1)
        {
            mouth.SetBlendShapeWeight(0,Mathf.Lerp(mouth.GetBlendShapeWeight(0),0, _y));
            mouth.SetBlendShapeWeight(0,Mathf.Lerp(mouth.GetBlendShapeWeight(1),0, _y));

            _y += Time.deltaTime ;
            yield return null;
        }
    }
    
    IEnumerator Sad()
    {
        float _y = 0;

        while (_y < 1)
        {
            mouth.SetBlendShapeWeight(0,Mathf.Lerp(mouth.GetBlendShapeWeight(0),0, _y));
            mouth.SetBlendShapeWeight(1,Mathf.Lerp(mouth.GetBlendShapeWeight(1),100, _y));

            _y += Time.deltaTime ;
            yield return null;
        }
    }
   
}
