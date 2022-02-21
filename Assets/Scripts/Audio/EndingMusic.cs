using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingMusic : MonoBehaviour
{
    private GameObject goAudioManager;
    private AudioManager AudioManager;
    private void Start()
    {
        goAudioManager = GameObject.Find("AudioManager");
        AudioManager = goAudioManager.GetComponent<AudioManager>();
        AudioManager.Stop("MainMusic");
        AudioManager.Play("EndingMusic");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
