using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

namespace Game
{



    public class MainMenu : MonoBehaviour
    {

        public GameObject mainMenu;
        public GameObject settingsMenu;
        public GameObject creditsMenu;
        public Slider volSlider;
        public AudioMixer mainMixer;
        private float volume;
        [SerializeField, Range(0.1f, 10f)] float amountToScaleEachTick = 0.1f;
        [SerializeField] Animator vignette;
        [SerializeField] GameObject fullScreenText;
        void Start()
        {
            vignette.SetTrigger("Main");
            volume = PlayerPrefs.GetFloat("volume");
            volSlider.value = volume;
            mainMixer.SetFloat("MainVolume", volume);

        }

        void Update()
        {
            if(Screen.fullScreen)
            {
                fullScreenText.SetActive(false);
            }
            else
            {
                fullScreenText.SetActive(true);
            }
        }

        public void StartGame()
        {
            StartCoroutine(this.StartVignette());

        }

        public IEnumerator StartVignette()
        {
            GameObject.Find("VignetteEffect").transform.SetAsLastSibling();
            GameObject.Find("VignetteEffect").GetComponent<VignetteEffect>().CloseVignette();
            yield return new WaitForSeconds(2.1f);


            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void Settings()
        {
            mainMenu.SetActive(false);
            settingsMenu.SetActive(true);
        }

        public void Credits()
        {
            mainMenu.SetActive(false);
            creditsMenu.SetActive(true);
        }

        public void Exit()
        {
            Application.Quit();
        }

        public void Back()
        {
            creditsMenu.SetActive(false);
            settingsMenu.SetActive(false);
            mainMenu.SetActive(true);
        }

        public void SetVolume(float vol)
        {
            mainMixer.SetFloat("MainVolume", vol);
            PlayerPrefs.SetFloat("volume", vol);
        }

    }
}