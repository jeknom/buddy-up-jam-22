using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VignetteSound : MonoBehaviour
{
    private GameObject goAudioManager;
    private AudioManager AudioManager;
    private bool hasPlayed;

    private void Start()
    {
        goAudioManager = GameObject.Find("AudioManager");
        AudioManager = goAudioManager.GetComponent<AudioManager>();
        hasPlayed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !hasPlayed)
        {
            AudioManager.Play("LevelClear");
            hasPlayed = true;
        }
    }
}
