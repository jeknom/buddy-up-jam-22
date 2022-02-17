using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class StatusUI : MonoBehaviour
    {
        [Header("Setup")]
        public Text status;

        [Header("Modifiers")]
        [SerializeField] float removeDuration;

        public void Remove() => StartCoroutine(this.RemoveInternal());

        IEnumerator RemoveInternal()
        {
            var timeRemaining = removeDuration;
            while (timeRemaining > 0)
            {
                var deltaTime = Time.deltaTime;
                timeRemaining -= deltaTime;
                var currentScale = this.transform.localScale;
                var scale = timeRemaining / this.removeDuration;
                var newX = currentScale.x * scale;
                var newY = currentScale.y * scale;
                var newScale = new Vector3(newX, newY, 0f);
                this.transform.localScale = newScale;
                yield return new WaitForSeconds(deltaTime);
            }

            Destroy(this.gameObject);
        }
    }
}
