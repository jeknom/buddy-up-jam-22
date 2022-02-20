using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VignetteSound : MonoBehaviour
{
    private GameObject goAudioManager;
    private AudioManager AudioManager;

    private void Start()
    {
        goAudioManager = GameObject.Find("AudioManager");
        AudioManager = goAudioManager.GetComponent<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AudioManager.Play("LevelClear");
        }
    }
}
