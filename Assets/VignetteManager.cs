using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class VignetteManager : MonoBehaviour
    {
        [SerializeField] GameObject vignetteEffect;

        public UnityEvent onVignetteClosed;

        public void InvokeClosed()
        {
            this.onVignetteClosed.Invoke();
        }

        void Start()
        {
            this.vignetteEffect.SetActive(true);
        }
    }
}
