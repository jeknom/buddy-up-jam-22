using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class BeetleMask : MonoBehaviour
    {
        [SerializeField] VignetteManager manager;

        void Start()
        {
            if (manager == null)
            {
                Debug.LogError("Beetle mask missing a manager");
            }
        }

        public void OnCloseAnimationComplete() => this.manager.InvokeClosed();
    }
}
