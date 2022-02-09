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
        [SerializeField] GameObject mask;

        void start()
        {
            volume = PlayerPrefs.GetFloat("volume");
            volSlider.value = volume;
            mainMixer.SetFloat("MainVolume", volume);
          
        }

        void FixedUpdate()
        {

        }

        public void StartGame()
        {
            mainMenu.SetActive(false);
            StartCoroutine(this.StartVignette());
            
        }

            public IEnumerator StartVignette()
        {


            while (mask.transform.localScale.x > 0f)
            {
                var scaler = this.amountToScaleEachTick * Time.deltaTime;
                mask.transform.localScale -= new Vector3(scaler, scaler, 0f);

                yield return new WaitForFixedUpdate();
            }
            mask.transform.localScale = new Vector3(0f, 0f, 0f);
            yield return new WaitForSeconds(0.5f);
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