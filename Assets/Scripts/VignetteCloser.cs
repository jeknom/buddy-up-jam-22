using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class VignetteCloser : MonoBehaviour
    {
        [SerializeField] VignetteEffect effectBehaviour;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                effectBehaviour.OpenVignette(false);
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                effectBehaviour.CloseVignette();
            }
        }

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == "Player")
            {
                this.effectBehaviour.CloseVignette();
            }
        }
    }
}