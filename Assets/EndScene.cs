using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class EndScene : MonoBehaviour
{
    [SerializeField] Animator anim;
    public UnityEvent onBeetleDone;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(GameObject.Find("GameManager"));

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void StartAnim()
    {
        anim.SetTrigger("Ending");
        //GameObject.Find("AlienBeetle").GetComponent<VignetteManager>().onVignetteClosed.AddListener(() => SceneManager.LoadScene("MainMenu"));
    }





}
