using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    public Renderer rend;
    public Material mat;

    public AnimationCurve curve;

    public List<Texture> slides;
    public int slideIndex;

    public bool sliding;

    public float speed = 3;

    public KeyCode inputNext;
    public KeyCode inputPrevious;
    
    // Start is called before the first frame update
    void Start()
    {
        mat = Instantiate(mat);
        rend.material = mat;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(inputNext) && !sliding && slideIndex<slides.Count-1)
        {
            StartCoroutine(Next());
        }
        if (Input.GetKeyDown(inputPrevious) && !sliding && slideIndex>0)
        {
            StartCoroutine(Previous());
        }
    }

    IEnumerator Next()
    {
        sliding = true;
        mat.SetTexture("_Panel01", slides[slideIndex+1]);
        
        float _y = 0;

        while (_y < 1)
        {
            mat.SetFloat("_Scroll", curve.Evaluate(_y));
            _y += Time.deltaTime *speed;
            yield return null;
        }
        _y = 1;
        mat.SetFloat("_Scroll", _y);
        mat.SetTexture("_Panel02", slides[slideIndex+1]);
        mat.SetFloat("_Scroll", 0);
        slideIndex++;
        sliding = false;
    }

    IEnumerator Previous()
    {
        sliding = true;
        mat.SetTexture("_Panel02", slides[slideIndex-1]);
        
        float _y = 1;

        while (_y >0)
        {
            mat.SetFloat("_Scroll", curve.Evaluate(_y));
            _y -= Time.deltaTime * speed;
            yield return null;
        }
        _y = 0;
        mat.SetFloat("_Scroll", _y);
        mat.SetTexture("_Panel01", slides[slideIndex-1]);
        mat.SetFloat("_Scroll", 0);
        slideIndex--;
        sliding = false; 
    }
}
