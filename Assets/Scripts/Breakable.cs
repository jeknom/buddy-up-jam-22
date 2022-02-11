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
            yield return new WaitForSeconds(this.breakDelay);

            Destroy(this.gameObject);
        }
    }
}