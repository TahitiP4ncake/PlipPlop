using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Referencies")]
    public AudioSource source;
    [Header("Inputs")]
    public KeyCode play;
    public KeyCode soundUp;
    public KeyCode soundDown;
    [Header("Settings")]
    public float volumeStep = 0.05f;

    float cVolume = 1;

    void Start()
    {
        cVolume = source.volume;
    }

    void Update()
    {
        if(Input.GetKeyDown(play)) 
        {
            if(!source.isPlaying) source.Play();
            else source.Pause();
        }
        else if(Input.GetKeyDown(soundUp)) ChangeVolume(volumeStep);
        else if(Input.GetKeyDown(soundDown)) ChangeVolume(-volumeStep);
    }

    void ChangeVolume(float value)
    {
        cVolume = Mathf.Clamp(cVolume + value, 0f, 1f);
        source.volume = cVolume;
    }
}
