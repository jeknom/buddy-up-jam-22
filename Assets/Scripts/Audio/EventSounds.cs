using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSounds : MonoBehaviour
{
    private GameObject goAudioManager;
    private AudioManager AudioManager;

    // Start is called before the first frame update
    void Start()
    {
        goAudioManager = GameObject.Find("AudioManager");
        AudioManager = goAudioManager.GetComponent<AudioManager>();
    }

    public void PlayBreakableSound()
    {
        AudioManager.Play("Breakable");
    }

    public void PlayPressurePlateSound()
    {
        AudioManager.Play("PressurePlate");
        Debug.Log("Played PressurePlate sound");
    }
}
