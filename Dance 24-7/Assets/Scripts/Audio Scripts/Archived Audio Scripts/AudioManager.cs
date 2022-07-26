using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using FMOD.Studio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public EventReference EventReference;

    private static FMOD.Studio.EventInstance Music;
    void Play()
    {
        
    }

    void Start()
    {
        Music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Chira");
        Music.start();

    }


    void Update()
    {
        
    }
}
