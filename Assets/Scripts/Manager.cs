using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public static Manager SINGLETON;

    [Space] [Header("UI")] 
    
    public GameObject gameUI;

    public TextMeshProUGUI zoneText;    
    public TextMeshProUGUI missionText; 
    
    


    [Space] [Header("Transition")] 
    
    public Material transitionMat;


    [Header("Subtitle")] 
    
    public TextMeshProUGUI subtitleText;

    private Coroutine subtitleCor = null;
    
    void Awake()
    {
        transitionMat.SetFloat("_Scale",610);

        
        if (SINGLETON == null)
        {
            SINGLETON = this;
        }
        else
        {
            Destroy(gameObject);
        }
        

    }
    
    void Start()
    {

      
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        

        StartLevel();

        Color _col = subtitleText.color;
        _col.a = 0;
        subtitleText.color= _col;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
        /*
            if (Cursor.visible)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            */

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }


    public void ShowUI()
    {
        StartCoroutine(Show());
    }

    public void HideUI()
    {
        StartCoroutine(Hide());
    }

    IEnumerator Show()
    {
        print("show UI");
        
        float _y = 0;

        Color _col = zoneText.color;
        _col.a = _y;
        
        while (_y < 1)
        {
            zoneText.color = _col;
            missionText.color = _col;

            _col.a = _y;
            _y += Time.deltaTime*3;
            yield return null;
        }

        _col.a = 1;
        
        zoneText.color = _col;
        missionText.color = _col;

    }

    IEnumerator Hide()
    {
        float _y = 1;

        Color _col = zoneText.color;
        
        _col.a = _y;
        
        while (_y >0)
        {
            zoneText.color = _col;
            missionText.color = _col;


            _col.a = _y;
            _y -= Time.deltaTime*3;
            yield return null;
        }

        _col.a = 0;
        
        zoneText.color = _col;
        missionText.color = _col;
    }

    public void StartLevel()
    {
        StartCoroutine(StartLevelCor());
    }
    
    public void EndLevel()
    {
        StartCoroutine(EndLevelCor());
    }
    
    IEnumerator StartLevelCor()
    {
        //print("transition");
        float _y = 1;

        while (_y >0)
        {
            transitionMat.SetFloat("_Scale",Mathf.Lerp(0,20,_y));
            
            _y -= Time.deltaTime;
            yield return null;
        }
        transitionMat.SetFloat("_Scale",0);
        
        Show();
        
        yield return new WaitForSecondsRealtime(2f);

        HideUI();
    }
    
    IEnumerator EndLevelCor()
    {
        float _y = 0;

        
        while (_y <1)
        {
      transitionMat.SetFloat("_Scale",Mathf.Lerp(0,20,_y));
            
            _y += Time.deltaTime*3;
            yield return null;
        }
        transitionMat.SetFloat("_Scale",20);

    }

    public void SubtitleOn(string _text)
    {

        if (subtitleCor != null)
        {
            StopCoroutine(subtitleCor);
        }
        
        subtitleText.text = _text;

        
        
        subtitleCor = StartCoroutine(SubtitleOnCor());
        
        
        
    }

    IEnumerator SubtitleOnCor()
    {
        float _y = subtitleText.color.a;

        Color _col = subtitleText.color;

        while (_y < 1)
        {
            subtitleText.color = _col;
            
            _col.a = _y;
            
            _y += Time.deltaTime*3;
            yield return null;
        }

        _y = 1;
        _col.a = _y;

        subtitleText.color = _col;
        
        yield return new WaitForSecondsRealtime(1.5f);
        
        while (_y >0)
        {
            subtitleText.color = _col;
            
            _col.a = _y;
            
            _y -= Time.deltaTime*3;
            yield return null;
        }
        
        _y = 0;
        _col.a = _y;

        subtitleText.color = _col;



        subtitleCor = null;
    }

}
