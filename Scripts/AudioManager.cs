using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    
    public classSound[] sounds;

    void Awake()
    {
        


        foreach(classSound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clipName;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

        }
    }


    public void play(string name)
    {
       classSound s = Array.Find(sounds, classSound => classSound.name == name);
        if (s == null)
        {
            Debug.LogWarning("No Sound of : " + name + " name was found");
            return;
        }
        s.source.Play();
    }
}
