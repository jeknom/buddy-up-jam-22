using System.Collections;
using UnityEngine;

namespace Game
{
    public class VignetteEffect : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] GameObject mask;

        [Header("Modifiers")]
        [SerializeField] Vector3 openScale = new Vector3(6f, 6f, 0f);
        [SerializeField] Vector3 closeScale = new Vector3(0f, 0f, 0f);
        [SerializeField, Range(0.1f, 10f)] float amountToScaleEachTick = 0.1f;
        [SerializeField] float startDelayInSeconds = 1f;

        public void OpenVignette(bool addStartDelay) => StartCoroutine(this.OpenVignetteRoutine(addStartDelay));
        public void CloseVignette() => StartCoroutine(this.CloseVignetteRoutine());

        bool isShifting = false;

        void Start()
        {
            this.mask.transform.localScale = this.closeScale;
            StartCoroutine(this.OpenVignetteRoutine(true));
        }

        IEnumerator OpenVignetteRoutine(bool addStartDelay)
        {
            if (this.isShifting)
            {
                yield break;
            }

            this.isShifting = true;

            if (addStartDelay)
            {
                yield return new WaitForSeconds(this.startDelayInSeconds);
            }

            while (this.mask.transform.localScale.x < this.openScale.x)
            {
                var scaler = this.amountToScaleEachTick * Time.deltaTime;
                this.mask.transform.localScale += new Vector3(scaler, scaler, 0f);

                yield return new WaitForFixedUpdate();
            }

            this.isShifting = false;
        }

        IEnumerator CloseVignetteRoutine()
        {
            if (this.isShifting)
            {
                yield break;
            }

            this.isShifting = true;

            while (this.mask.transform.localScale.x > this.closeScale.x)
            {
                var scaler = this.amountToScaleEachTick * Time.deltaTime;
                this.mask.transform.localScale -= new Vector3(scaler, scaler, 0f);

                yield return new WaitForFixedUpdate();
            }

            this.isShifting = false;
        }
    }
}