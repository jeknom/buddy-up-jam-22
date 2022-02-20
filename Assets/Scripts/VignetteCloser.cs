using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class VignetteCloser : MonoBehaviour
    {
        [SerializeField] VignetteManager manager;
        [SerializeField] VignetteEffect effectBehaviour;

        void Start()
        {
            if (manager == null)
            {
                Debug.Log("Closer missing manager");
            }

            if (effectBehaviour == null)
            {
                Debug.Log("Effect behaviour missing");
            }

            
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == "Player")
            {
                manager.onVignetteClosed.AddListener(() => GameObject.FindObjectOfType<GameManager>().NextLevel());
                this.effectBehaviour.CloseVignette();
            }
        }
    }
}