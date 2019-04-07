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
                    return;
                }
            }
            
            PreviousSlide();
        }
    }

    void NextSlide()
    {
        

             ChangeState();
        


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
        slides[slideIndex].SetActive(false);
    }
}
