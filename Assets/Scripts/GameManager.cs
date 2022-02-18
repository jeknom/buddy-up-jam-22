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
        // Start is called before the first frame update
        void Start()
        {
            DontDestroyOnLoad(transform.gameObject);
            volume = PlayerPrefs.GetFloat("volume");
            // volSlider.value = volume;
            mainMixer.SetFloat("MainVolume", volume);
        }

        // Update is called once per frame
        void Update()
        {

        }


        public void NextLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void Death()
        {
            StartCoroutine(WaitForDeath());

        }


        IEnumerator WaitForDeath()
        {

            GameObject.Find("VignetteEffect").GetComponent<VignetteEffect>().CloseVignette();
            mainAudioSource.PlayOneShot(deathSound);
            yield return new WaitForSeconds(3);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Destroy(this.gameObject);
        }


    }

}
