using System.Collections;
using UnityEngine;

namespace Game
{
    public class VignetteEffect : MonoBehaviour
    {
        [SerializeField] Animator effectAnimator;

        public void OpenVignette()
        {
            this.effectAnimator.SetTrigger("Open");
        }

        public void CloseVignette()
        {
            this.effectAnimator.SetTrigger("Close");
        }
    }
}