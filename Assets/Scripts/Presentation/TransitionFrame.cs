using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class FrameAnimation
{
    public string name;
    public AnimationCurve curve;
    public float duration;
}

public class TransitionFrame : MonoBehaviour
{
    public FrameAnimation[] animations;

    Image img;
    FrameAnimation currentAnimation;
    float timer = 0f;

    public event System.Action OnFrameAnimationStart;
    public event System.Action OnFrameAnimationEnd;

    public void Awake()
    {
        img = GetComponent<Image>();
        img.material = Instantiate(img.material);
    }

    public void Update()
    {
        if(currentAnimation != null)
        {
            if(timer <= currentAnimation.duration)
            {
                timer += Time.deltaTime;
                img.material.SetFloat("_Value", currentAnimation.curve.Evaluate(timer/currentAnimation.duration));
            }
            else
            {
                currentAnimation = null;
                OnFrameAnimationEnd?.Invoke();
            }
        }
    }

    public void PlayFrameAnimation(string name)
    {
        timer = 0;
        currentAnimation = GetFrameAnimation(name);
        OnFrameAnimationStart?.Invoke();
    }
    public void PlayFrameAnimation(FrameAnimation FrameAnimation)
    {
        timer = 0;
        currentAnimation = FrameAnimation;
        OnFrameAnimationStart?.Invoke();
    }

    FrameAnimation GetFrameAnimation(string name)
    {
        foreach(FrameAnimation fa in animations)
        {
            if(fa.name == name) return fa;
        }
        return null;
    }
}
