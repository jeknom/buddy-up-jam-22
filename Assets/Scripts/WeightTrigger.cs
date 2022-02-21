using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    [RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
    public class WeightTrigger : MonoBehaviour
    {
        public const string DUNG_BALL_TAG = "DungBall";

        [Header("Setup")]
        public UnityEvent onEnoughDungBallMass;
        // This fires with the argument of how close it is to breaking. 0.1 = 10%, 0.5 = 50%, 1 = 100% and so on.
        public UnityEvent<float> onNotEnoughBallMass;
        public UnityEvent onBallExit;

        [Header("Modifiers")]
        [Range(1f, 20f)] public float triggerMass;

        private void OnCollisionStay2D(Collision2D collision)
        {
            var collider = collision.collider;
            if (collider.tag == DUNG_BALL_TAG && collider.attachedRigidbody.mass >= this.triggerMass)
            {
                this.onEnoughDungBallMass.Invoke();
            }
            else if (collider.tag == DUNG_BALL_TAG)
            {
                this.onNotEnoughBallMass.Invoke(collider.attachedRigidbody.mass / this.triggerMass);
            }
        }

        void OnCollisionExit2D(Collision2D collision)
        {
            var collider = collision.collider;
            if (collider.tag == DUNG_BALL_TAG)
            {
                this.onBallExit.Invoke();
            }
        }
    }
}