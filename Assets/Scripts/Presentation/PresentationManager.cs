using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PresentationManager : MonoBehaviour
{
    [Header("Referencies")]
    public string[] scenes;
    public TransitionFrame tf;
    int current = 0;

     [Header("Inputs")]
    public KeyCode next;
    public KeyCode previous;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        //Load(current);

        tf.OnFrameAnimationEnd += () => { Transition(); };
    }

    public void Transition()
    {
        if(scenes[current] != SceneManager.GetActiveScene().name)
        {
            this.Load(current);
            this.tf.PlayFrameAnimation("open");
        }
    } 

    public void Next()
    {
        current++;
        if(current >= scenes.Length) current = 0;
        tf.PlayFrameAnimation("close");
    }

    public void Previous()
    {
        current--;
        if(current < 0) current = scenes.Length - 1;
        tf.PlayFrameAnimation("close");
    }

    void Load(int index)
    {
        SceneManager.LoadScene(scenes[index]);
    }

    void Update()
    {
        if(Input.GetKeyDown(next)) Next();
        else if(Input.GetKeyDown(previous)) Previous();
    }
}
