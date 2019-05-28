using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class PlayVideo : MonoBehaviour
{
    [Header("Referencies")]
    public VideoPlayer vp;
    public RawImage screen;
    [Header("Inputs")]
    public KeyCode play;
    public KeyCode restart;
    public KeyCode soundUp;
    public KeyCode soundDown;
    [Header("Settings")]
    public float volumeStep = 0.05f;

    float cVolume = 1;

    void Start()
    {
        cVolume = vp.GetDirectAudioVolume(0);
        RenderTexture rt = new RenderTexture(1920, 1080, 16, RenderTextureFormat.ARGB32);
        vp.targetTexture = rt;
        screen.texture = vp.targetTexture;
    }

    void Update()
    {
        if(Input.GetKeyDown(play)) 
        {
            if(!vp.isPlaying) vp.Play();
            else vp.Pause();
        }
        else if(Input.GetKeyDown(restart)) vp.time = 0f;
        else if(Input.GetKeyDown(soundUp)) ChangeVolume(volumeStep);
        else if(Input.GetKeyDown(soundDown)) ChangeVolume(-volumeStep);
    }

    void ChangeVolume(float value)
    {
        cVolume = Mathf.Clamp(cVolume + value, 0f, 1f);
        vp.SetDirectAudioVolume(0, cVolume);
    }
}
