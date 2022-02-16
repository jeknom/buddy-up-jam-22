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

        [Header("Modifiers")]
        [SerializeField] Color deathColor = Color.red;

        Animator playerAnimator;
        SpriteRenderer playerRenderer;
        bool isFacingRight = true;

        void Awake()
        {
            this.playerAnimator = this.GetComponent<Animator>();
            this.playerRenderer = this.GetComponent<SpriteRenderer>();
            this.movementBehaviour.onUpdateCollisions.RemoveListener(UpdateAnimations);
            this.movementBehaviour.onUpdateCollisions.AddListener(UpdateAnimations);
            this.movementBehaviour.onPlayerDestroy.AddListener(() => this.playerRenderer.color = this.deathColor);
        }

        void UpdateAnimations(List<Movement.PlayerCollision> collisions)
        {
            var isGrounded = Movement.IsGrounded(collisions);
            var velocity = this.movementBehaviour.Velocity;
            var isMovingHorizontally = Math.Abs(velocity.x) > 0.01f;
            var isFacingRight = velocity.x > 0f;
            var isPushing = false;

            foreach (var collision in collisions)
            {
                var isCollidingLeftOrRight = collision.direction == Vector2.left || collision.direction == Vector2.right;
                var newValue = collision.tag == "DungBall" && isCollidingLeftOrRight;

                if (isPushing != newValue)
                {
                    isPushing = newValue;
                    break;
                }
            }

            this.SetPushing(isGrounded && isPushing);
            this.SetInAir(!isGrounded);

            if (isMovingHorizontally)
            {
                this.SetRunning(isFacingRight);
            }
            else if (isGrounded)
            {
                this.SetIdle();
            }
        }

        void SetRunning(bool isFacingRight)
        {
            if (!isFacingRight && !this.playerRenderer.flipX)
            {
                this.playerRenderer.flipX = true;
            }
            else if (isFacingRight && this.playerRenderer.flipX)
            {
                this.playerRenderer.flipX = false;
            }

            if (this.playerAnimator.GetBool("IsRunning"))
            {
                return;
            }

            this.playerAnimator.SetBool("IsRunning", true);
        }

        void SetIdle()
        {
            if (!this.playerAnimator.GetBool("IsRunning"))
            {
                return;
            }

            this.playerAnimator.SetBool("IsRunning", false);
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
            Debug.Log("Animator set pushing " + this.playerAnimator.GetBool("IsPushing"));
            if (isPushing == this.playerAnimator.GetBool("IsPushing"))
            {
                return;
            }

            this.playerAnimator.SetBool("IsPushing", isPushing);
        }
    }
}