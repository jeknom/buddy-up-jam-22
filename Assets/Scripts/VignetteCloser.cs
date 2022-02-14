using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class VignetteCloser : MonoBehaviour
    {
        [SerializeField] VignetteEffect effectBehaviour;

        void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.tag == "Player")
            {
                this.effectBehaviour.CloseVignette();
            }
        }
    }
}