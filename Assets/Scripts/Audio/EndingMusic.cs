using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingMusic : MonoBehaviour
{
    private GameObject goAudioManager;
    private AudioManager AudioManager;
    [SerializeField] private string musicToPlay;
    [SerializeField] private string musicToStop;
    private void Start()
    {
        goAudioManager = GameObject.Find("AudioManager");
        AudioManager = goAudioManager.GetComponent<AudioManager>();
        AudioManager.Stop(musicToStop);
        AudioManager.Play(musicToPlay);
    }

}
