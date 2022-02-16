using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public class PlayerAnimation : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] Movement movementBehaviour;
        [SerializeField] PlayerInput playerInput;

        [Header("Modifiers")]
        [SerializeField] Color deathColor = Color.red;

        Animator playerAnimator;
        SpriteRenderer playerRenderer;

        void Awake()
        {
            this.playerAnimator = this.GetComponent<Animator>();
            this.playerRenderer = this.GetComponent<SpriteRenderer>();

            this.movementBehaviour.onUpdateCollisions.RemoveListener(UpdateAnimations);
            this.movementBehaviour.onUpdateCollisions.AddListener(UpdateAnimations);

            this.movementBehaviour.onPlayerDestroy.RemoveListener(this.SetDeath);
            this.movementBehaviour.onPlayerDestroy.AddListener(this.SetDeath);

            this.playerInput.onChangeDirection.RemoveListener(this.Flip);
            this.playerInput.onChangeDirection.AddListener(this.Flip);
        }

        void UpdateAnimations(List<Movement.PlayerCollision> collisions)
        {
            var isGrounded = Movement.IsGrounded(collisions);
            var velocity = this.movementBehaviour.Velocity;
            var isMovingHorizontally = Math.Abs(velocity.x) > 0.01f;
            var isPushing = false;

            foreach (var collision in collisions)
            {
                var isFacingRight = !this.playerRenderer.flipX;
                var isFacingRightAndCollidingRight = isFacingRight && collision.direction == Vector2.right;
                var isFacingLeftAndCollidingLeft = !isFacingRight && collision.direction == Vector2.left;
                var isCollidingLeftOrRight = isFacingLeftAndCollidingLeft || isFacingRightAndCollidingRight;
                var newValue = collision.tag == "DungBall" && isCollidingLeftOrRight && isMovingHorizontally;

                if (isPushing != newValue)
                {
                    isPushing = newValue;
                    break;
                }
            }

            this.SetPushing(isPushing);
            this.SetInAir(!isGrounded);
            this.SetRunning(isMovingHorizontally);
        }

        void Flip()
        {
            Debug.Log("Flipping");
            this.playerRenderer.flipX = !this.playerRenderer.flipX;
        }

        void SetDeath()
        {
            this.playerRenderer.color = this.deathColor;
        }

        void SetRunning(bool isRunning)
        {
            if (this.playerAnimator.GetBool("IsRunning") == isRunning)
            {
                return;
            }

            this.playerAnimator.SetBool("IsRunning", isRunning);
        }

        void SetInAir(bool isInAir)
        {
            if (isInAir == this.playerAnimator.GetBool("IsInAir"))
            {
                return;
            }

            this.playerAnimator.SetBool("IsInAir", isInAir);
        }

        void SetPushing(bool isPushing)
        {
            if (isPushing == this.playerAnimator.GetBool("IsPushing"))
            {
                return;
            }

            this.playerAnimator.SetBool("IsPushing", isPushing);
        }
    }
}