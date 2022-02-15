using System;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.output;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            
        }
    }

    public void Play(string name)
    {        
        FindSound(name).source.Play();
    }

    public void SetVolume(string name, float volume)
    {     
        FindSound(name).source.volume = volume;
    }

    public void SetPitch(string name, float pitch)
    {
        FindSound(name).source.pitch = pitch;
    }

    private Sound FindSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found !");
        }
        return s;
    }

}
