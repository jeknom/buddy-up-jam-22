using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


namespace Game
{


    public class PauseGame : MonoBehaviour
    {
        [SerializeField] GameObject pauseMenuCanvas;
        [SerializeField] AudioClip pauseIn;
        [SerializeField] AudioClip pauseOut;
        [SerializeField] AudioClip mainMenuClip;
        [SerializeField] AudioClip continueClip;
        [SerializeField] AudioClip deathSound;
        [SerializeField] AudioSource UIaudioSource;
        public Slider volSlider;
        public AudioMixer mainMixer;
        private float volume;


        private bool isPaused = false;

        // Start is called before the first frame update
        void Start()
        {
            volume = PlayerPrefs.GetFloat("volume");
            volSlider.value = volume;
            mainMixer.SetFloat("MainVolume", volume);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {

                isPaused = !isPaused;

                Pause(isPaused);


            }
        }


        void Pause(bool paused)
        {

            pauseMenuCanvas.SetActive(paused);

            if (paused == true)
            {
                Time.timeScale = 0;
                UIaudioSource.PlayOneShot(pauseIn);

            }
            else
            {
                UIaudioSource.PlayOneShot(pauseOut);
                Time.timeScale = 1;
            }


        }

        public void Continue()
        {
            UIaudioSource.PlayOneShot(continueClip);
            isPaused = false;
            pauseMenuCanvas.SetActive(false);
            Time.timeScale = 1;
        }

        public void Main()
        {

            StartCoroutine(WaitForClipMenu());



        }


        IEnumerator WaitForClipMenu()
        {
            Time.timeScale = 1;
            pauseMenuCanvas.SetActive(false);
            GameObject.Find("VignetteEffect").GetComponent<VignetteEffect>().CloseVignette();
            UIaudioSource.PlayOneShot(mainMenuClip);
            yield return new WaitForSeconds(2.5f);
            SceneManager.LoadScene("MainMenu");
            Destroy(gameObject);
            


        }




        public void Restart()
        {
            Time.timeScale = 1;
            pauseMenuCanvas.SetActive(false);
            GameObject.Find("Player").GetComponent<Movement>().onPlayerDestroy.Invoke();
            GameObject.Find("GameManager").GetComponent<GameManager>().Death();
        }

        public void Exit()
        {
            Application.Quit();
        }


        public void SetVolume(float vol)
        {
            mainMixer.SetFloat("MainVolume", vol);
            PlayerPrefs.SetFloat("volume", vol);
        }


    }

}