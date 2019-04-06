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

    public GameObject answerObject;
    
    public TextMeshProUGUI answerText;

    public GameObject right;
    public GameObject left;
    
    
    //SON

    [Header("SON")] 
    
    public string[] sonsName;
    public AudioClip[] sonsFiles;
    
    Dictionary<string, AudioClip> sons = new Dictionary<string, AudioClip>();

    
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
        for (int i = 0; i < sonsName.Length; i++)
        {
            sons.Add(sonsName[i], sonsFiles[i]);
        }
        
        
        
        
      //??
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        

        StartLevel();

        Color _col = subtitleText.color;
        _col.a = 0;
        subtitleText.color= _col;

        //answerObject.SetActive(false);
        answerText.text = "";

        right.SetActive(false);
        left.SetActive(false);
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

    public void SubtitleOn(string _text , bool _fade)
    {

        if (subtitleCor != null)
        {
            StopCoroutine(subtitleCor);
        }
        
        subtitleText.text = _text;

        
        
        subtitleCor = StartCoroutine(SubtitleOnCor(_fade));
        
        
        
    }

    IEnumerator SubtitleOnCor(bool _fade)
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

        if (!_fade)
        {
            yield break;
        }
        
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

    public void EndConversation(bool _scream = false)
    {
        right.SetActive(false);
        left.SetActive(false);
        
        answerText.text = "";

        if (_scream)
        {
            return;
        }
        
        
        if (subtitleCor != null)
        {
            StopCoroutine(subtitleCor);

        }
        
        subtitleCor = StartCoroutine(HideSubtitle());
    }
    
    IEnumerator HideSubtitle()
    {

        float _y = 1;

        Color _col = subtitleText.color;
        
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


    public void AnswerOn()
    {
        //answerObject.SetActive(true);
        answerText.text = "plip";
        right.SetActive(true);
    }

    public void ChangeAnswer(int _index)
    {
        if (_index == 1)
        {
            right.SetActive(false);
            left.SetActive(true);
            answerText.text = "plop";
        }
        else
        {
            right.SetActive(true);
            left.SetActive(false);

            answerText.text = "plip";

        }
    }

    public void AnswerOff()
    {
        answerText.text = "";

        right.SetActive(false);
        left.SetActive(false);
        // answerObject.SetActive(false);

    }

    public void PlaySound(string _clip, float _volume = 1)
    {
        AudioSource _son = new GameObject().AddComponent<AudioSource>();
        _son.Stop();
        _son.clip = sons[_clip];

        _son.volume = _volume;

        _son.pitch = Random.Range(.9f, 1.1f);

        _son.loop = false;


        _son.Play();
        
        Destroy(_son.gameObject,_son.clip.length);

    }
    

}
