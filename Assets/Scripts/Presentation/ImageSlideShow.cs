using UnityEngine;

public class ImageSlideShow : MonoBehaviour
{
    [Header("Referencies")]
    public Animation[] images;
    [Header("Inputs")]
    public KeyCode next;
    public KeyCode previous;

    bool[] shown;
    bool[] smaller;
    int current = -1;

    void Start()
    {
        shown = new bool[images.Length];
        smaller = new bool[images.Length];
        //Show(current);
    }

    public void Next()
    {
        current++;
        if(current >= images.Length) 
        {
            current = 0;
            HideOthers(-1);
        }
        else Show(current);
    }

    public void Previous()
    {
        current--;
        if(current < 0) current = images.Length - 1;
        Show(current);
    }

    public void Show(int index)
    {
        if(shown[index]) return;

        shown[index] = true;
        images[index].Play("Appear");
        HideOthers(index);
    }

    public void HideOthers(int index)
    {
        for(int i = 0; i < images.Length; i++)
        {
            if(shown[i] && !smaller[i] && i != index) 
            {
                images[i].Play("GetSmaller");
                smaller[i] = true;
            }
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(next)) Next();
        else if(Input.GetKeyDown(previous)) Previous();
    }
}
