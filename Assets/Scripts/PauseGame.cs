using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] GameObject pauseMenuCanvas;



    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        
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
        
        pauseMenuCanvas.SetActive(isPaused);

        if(paused == true)
        {
            Time.timeScale = 0;
        } else

            Time.timeScale = 1;

    }



}
