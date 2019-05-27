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
            if(_element!=null)
            _element.SetActive(false);
        }
    }


    public void NextElement()
    {
        if (elementIndex <= elements.Length - 1)
        {
            if (elements[elementIndex] == null)
            {
                foreach (var _e in elements)
                {
                    if(_e!=null)
                    _e.SetActive(false);
                }
            }
            else
            {

                elements[elementIndex].SetActive(true);

            }

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
