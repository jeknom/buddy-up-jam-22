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

        //Ab - Needed to pause the audio
        private GameObject goAudioManager;
        private MixerManager MixerManager;
        private AudioManager AudioManager;

        // Start is called before the first frame update
        void Start()
        {
            //Ab - audio
            goAudioManager = GameObject.Find("AudioManager");
            MixerManager = goAudioManager.GetComponent<MixerManager>();
            AudioManager = goAudioManager.GetComponent<AudioManager>();

            volume = PlayerPrefs.GetFloat("volume");
            volSlider.value = volume;
            mainMixer.SetFloat("MainVolume", volume);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P))
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

                MuteAllSounds(); //Ab

            }
            else
            {
                UIaudioSource.PlayOneShot(pauseOut);
                Time.timeScale = 1;

                UnMuteAllSounds(); //Ab
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

           
            UIaudioSource.PlayOneShot(mainMenuClip);
            GameObject.Find("StatusManger").GetComponent<VignetteManager>().onVignetteClosed.AddListener(() => SceneManager.LoadScene("MainMenu"));
            GameObject.Find("StatusManger").GetComponent<VignetteManager>().onVignetteClosed.AddListener(() => Destroy(gameObject));
            Time.timeScale = 1;
            pauseMenuCanvas.SetActive(false);
            GameObject.Find("VignetteEffect").GetComponent<VignetteEffect>().CloseVignette();
            Destroy(gameObject);

        }






        public void Restart()
        {
            Time.timeScale = 1;
            pauseMenuCanvas.SetActive(false);
            GameObject.Find("Player").GetComponent<Movement>().onPlayerDestroy.Invoke();
            GameObject.Find("GameManager").GetComponent<GameManager>().Death();

            if(SceneManager.GetActiveScene().name == "Level1")
            {
                Destroy(gameObject);
            }
            
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


        //Ab - audio
        public void MuteAllSounds()
        {
            MixerManager.SetVolume("Sounds", -80.0f); 
        }

        //Ab - audio
        public void UnMuteAllSounds()
        {
            MixerManager.SetVolume("Sounds", 0.0f); 
        }
    }

}