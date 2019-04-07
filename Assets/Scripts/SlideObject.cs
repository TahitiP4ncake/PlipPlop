using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideObject : MonoBehaviour
{
    public GameObject[] elements;

    public int elementIndex;


    public bool done;
    private void Start()
    {
        foreach (var _element in elements)
        {
            _element.SetActive(false);
        }
    }


    public void NextElement()
    {
        if (elementIndex <= elements.Length - 1)
        {
            elements[elementIndex].SetActive(true);

            elementIndex++;
        }
//        else
//        {
//            done = true;
//        }

        if (elementIndex > elements.Length - 1)
        {
            done = true;
        }

    }
}
