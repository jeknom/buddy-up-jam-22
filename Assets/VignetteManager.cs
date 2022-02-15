using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class VignetteManager : MonoBehaviour
    {
        [SerializeField] GameObject vignetteEffect;

        void Start()
        {
            this.vignetteEffect.SetActive(true);
        }
    }
}
