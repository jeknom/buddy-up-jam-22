using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

namespace Game
{
    [RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
    public class DungBall : MonoBehaviour
    {
        public const string OBSERVABLE_ID = "Dungball";
        public const string GROWER_TAG = "Grower";
        public const string SHRINKER_TAG = "Shrinker";

        [Header("Setup")]
        [SerializeField] LayerMask groundLayer;
        [SerializeField] LayerMask destroyLayer;

        [Header("Modifiers")]
        [SerializeField, Range(1f, 10f)] float maxSize = 10f;
        [SerializeField, Range(0.9f, 10f)] float minSize = 0.9f;
        [SerializeField, Range(1f, 100f)] float maxMass = 30f;
        [SerializeField, Range(0.01f, 1f)] float collisionRayLength = 0.2f;
        [SerializeField, Range(0.1f, 1f)] float scalingModifier = 0.5f;
        [SerializeField, Range(0.1f, 5f)] float destroySpeed = 1f;
        [SerializeField] string statusTextPrefix = "Ball mass:";

        [Header("Other")]
        [SerializeField] bool debug;
        [SerializeField] Rect debugPos = new Rect(new Vector2(0f, 0f), new Vector2(500f, 30f));
        public UnityEvent onBallDestroyed;
        public bool IsBallGrounded() => this.GetCollision(this.groundLayer);

        CircleCollider2D collider2d;
        Rigidbody2D rb2d;
        bool isDestroying;

        void Awake()
        {
            this.collider2d = this.GetComponent<CircleCollider2D>();
            this.rb2d = this.GetComponent<Rigidbody2D>();
        }

        void FixedUpdate()
        {
            if (this.isDestroying)
            {
                return;
            }

            var deathHit = this.GetCollision(this.destroyLayer);
            if (deathHit.collider != null)
            {
                this.onBallDestroyed.Invoke();
                this.isDestroying = true;
                StartCoroutine(this.DestroyRoutine());
            }

            var groundHit = this.GetCollision(this.groundLayer);
            if (groundHit.collider == null)
            {
                return;
            }

            var verticalVelocity = Mathf.Abs(this.rb2d.velocity.x);
            if (verticalVelocity <= 0f)
            {
                return;
            }

            var hitTag = groundHit.collider.tag;
            var sizeModifier = verticalVelocity * Time.deltaTime * this.scalingModifier;
            if (hitTag == SHRINKER_TAG)
            {
                sizeModifier = -sizeModifier;
            }
            // If tag is not either grower or shrinker we exit
            else if (hitTag != GROWER_TAG)
            {
                return;
            }

            var currentScale = this.transform.localScale.x;
            if ((sizeModifier > 0f && currentScale >= this.maxSize) ||
                (sizeModifier < 0f && currentScale <= this.minSize))
            {
                return;
            }


            var massPercentage = currentScale / this.maxSize;
            var sizeChange = new Vector3(sizeModifier, sizeModifier, 1f);

            this.rb2d.mass = this.maxMass * massPercentage;
            this.transform.localScale += sizeChange;
        }

        RaycastHit2D GetCollision(LayerMask mask)
        {
            var playerBounds = this.collider2d.bounds;
            var hit = Physics2D.Raycast(this.collider2d.bounds.center,
            Vector2.down,
            this.collider2d.bounds.extents.x + this.collisionRayLength,
            mask);

            if (hit && this.debug)
            {
                Debug.DrawLine(this.rb2d.position, hit.point, Color.red, .01f);
            }

            return hit;
        }

        IEnumerator DestroyRoutine()
        {
            while (this.transform.localScale.x > 0f)
            {
                this.transform.localScale -= new Vector3(this.destroySpeed, this.destroySpeed, 0f) * Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            GameObject.FindObjectOfType<GameManager>().Death();

            Destroy(this.gameObject);
        }

        void OnGUI()
        {
            if (this.debug)
            {
                var groundHit = this.GetCollision(this.groundLayer);
                var debugged = new List<(string, string)>
            {
                ( "Hit tag", groundHit.collider != null ? groundHit.collider.tag : "None" ),
                ( "Hit GameObject", groundHit.collider != null ? groundHit.collider.name : "None" ),
                ( "Scale", $"Min {this.minSize} | Max {this.maxSize} | Current {(int)this.transform.localScale.x} | {(this.transform.localScale.x / this.maxSize).ToString("#0.#%")}" ),
                ( "Mass", $"Max {this.maxMass} | Current {(int)this.rb2d.mass} | {(this.rb2d.mass / this.maxMass).ToString("#0.#%")}" )
            };

                var pos = this.debugPos;
                for (var i = 0; i < debugged.Count; i++)
                {
                    var item = debugged[i];
                    GUI.Label(pos, new GUIContent($"{item.Item1}: {item.Item2}"), new GUIStyle()
                    {
                        fontSize = 24
                    });
                    pos = GetLabelNextLine(pos);
                }
            }
        }

        static Rect GetLabelNextLine(Rect prev)
        {
            var rect = new Rect(new Vector2(0f, prev.position.y + 50f), prev.size);

            return rect;
        }
    }
}