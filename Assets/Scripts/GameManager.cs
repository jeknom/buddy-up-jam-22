using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

namespace Game
{

    public class GameManager : MonoBehaviour
    {
        [SerializeField] AudioClip deathSound;
        [SerializeField] AudioSource mainAudioSource;

        public AudioMixer mainMixer;
        private float volume;

        void Start()
        {
            DontDestroyOnLoad(transform.gameObject);
            volume = PlayerPrefs.GetFloat("volume");
            // volSlider.value = volume;
            mainMixer.SetFloat("MainVolume", volume);
        }

        public void NextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void Death()
        {
            GameObject.FindObjectOfType<VignetteEffect>().CloseVignette();
            mainAudioSource.PlayOneShot(deathSound);
            GameObject.FindObjectOfType<VignetteManager>().onVignetteClosed.AddListener(() =>
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
        }
    }
}
