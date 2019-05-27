using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PresentationManager : MonoBehaviour
{
    public string[] scenes;
    int current = 0;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Load(current);
    }

    public void Next()
    {
        current++;
        if(current >= scenes.Length) current = 0;
        Load(current);
    }

    public void Previous()
    {
        current--;
        if(current < 0) current = scenes.Length - 1;
        Load(current);
    }

    void Load(int index)
    {
        SceneManager.LoadScene(scenes[index]);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow)) Next();
        else if(Input.GetKeyDown(KeyCode.LeftArrow)) Previous();
    }
}
