using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slide : MonoBehaviour
{

    public GameObject[] slides;

    public int slideIndex;

    public bool slideOn;

    [Space] 
    
    public Image slideImage;

    public Animator anim;


    public SlideObject activeSlide;

    private void Start()
    {
        activeSlide = slides[0].GetComponent<SlideObject>();

    }

    void Update()
    {
        if (Input.GetButtonDown("GamepadStart"))
        {
            if (!slideOn)
            {

                if (slideIndex < slides.Length - 1)
                {
                    slideIndex++;
                }
                else
                {
                    
                    ShowSlide();
                    return;
                }
            }
            
            NextSlide();
        }

        if (Input.GetButtonDown("GamepadSelect"))
        {
            if (!slideOn)
            {
    
                if (slideIndex > 0)
                {
                    slideIndex--;
                }
                else
                {
                    
                    ShowSlide();
                    return;
                }
            }
            
            PreviousSlide();
        }
    }

    void NextSlide()
    {

        if (activeSlide.done || !slideOn)
        {
            ChangeState();
        }
        else
        {
            activeSlide.NextElement();
        }



    }

    void PreviousSlide()
    {
       

            ChangeState();
        
    }

    void ChangeState()
    {
        if (slideOn)
        {
            HideSlide();
        }
        else
        {
            ShowSlide();
        }
    }
    
    void ShowSlide()
    {
        //slideImage.sprite = slides[slideIndex];

        slides[slideIndex].SetActive(true);

        activeSlide = slides[slideIndex].GetComponent<SlideObject>();
        
        anim.SetTrigger("On");

        slideOn = true;

    }

    void HideSlide()
    {
        anim.SetTrigger("Out");
        


        slideOn = false;

    }

    public void HideSlideObject()
    {
        for (int i = 0; i < slides.Length; i++)
        {
            slides[i].SetActive(false);

        }
    }
}
