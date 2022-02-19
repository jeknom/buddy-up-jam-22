using System;
using UnityEngine;


public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

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

    private void Start()
    {
        Play("MainMusic");
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

    public void Stop(string name)
    {
        FindSound(name).source.Stop();
    }

    public void Pause(string name)
    {
        FindSound(name).source.Pause();
    }

    public void UnPause(string name)
    {
        FindSound(name).source.UnPause();
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
