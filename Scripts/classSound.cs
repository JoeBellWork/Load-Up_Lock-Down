using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


[System.Serializable]
public class classSound
{
    public string name;
    public AudioClip clipName;

    [Range(0f,2f)]
    public float volume;

    [Range(0.1f, 4f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;
}

    

