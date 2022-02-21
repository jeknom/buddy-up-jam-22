using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;

public class EndingMusic : MonoBehaviour
{
    private GameObject goAudioManager;
    private AudioManager AudioManager;
    private MixerManager MixerManager;
    [SerializeField] private string musicToPlay;
    [SerializeField] private string musicToStop;
    private void Start()
    {
        goAudioManager = GameObject.Find("AudioManager");
        AudioManager = goAudioManager.GetComponent<AudioManager>();
        MixerManager = goAudioManager.GetComponent<MixerManager>();
        AudioManager.Stop(musicToStop);
        AudioManager.Play(musicToPlay);

        MixerManager.SetVolume("Sounds", -80.0f); //mute all sounds
        
    }

}
