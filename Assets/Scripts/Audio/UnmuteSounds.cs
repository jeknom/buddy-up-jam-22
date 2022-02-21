using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnmuteSounds : MonoBehaviour
{
    private GameObject goAudioManager;
    private MixerManager MixerManager;

    // Start is called before the first frame update
    void Start()
    {
        goAudioManager = GameObject.Find("AudioManager");;
        MixerManager = goAudioManager.GetComponent<MixerManager>();

        MixerManager.SetVolume("Sounds", 0);
    }

}
