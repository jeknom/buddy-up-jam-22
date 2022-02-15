using System.Collections;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Breakable : MonoBehaviour
    {
        [Header("Modifiers")]
        [SerializeField, Range(0.1f, 3f)] float breakDelay;

        SpriteRenderer spriteRenderer;

        void Awake()
        {
            this.spriteRenderer = this.GetComponent<SpriteRenderer>();
        }

        public void Break() => this.StartCoroutine(this.BreakInternal());

        IEnumerator BreakInternal()
        {
            var passedDelay = this.breakDelay;
            while (passedDelay > 0)
            {
                var deltaTime = Time.deltaTime;
                passedDelay -= deltaTime;
                var percentOfTimePassed = passedDelay / this.breakDelay;
                var currentColor = this.spriteRenderer.color;
                currentColor.a = percentOfTimePassed;
                this.spriteRenderer.color = currentColor;

                yield return new WaitForSeconds(deltaTime);
            }


            Destroy(this.gameObject);
        }
    }
}