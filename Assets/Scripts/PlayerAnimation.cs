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

        Animator playerAnimator;
        SpriteRenderer playerRenderer;
        bool isFacingRight = true;

        void Awake()
        {
            this.playerAnimator = this.GetComponent<Animator>();
            this.playerRenderer = this.GetComponent<SpriteRenderer>();
            this.movementBehaviour.onUpdateCollisions.RemoveListener(UpdateAnimations);
            this.movementBehaviour.onUpdateCollisions.AddListener(UpdateAnimations);
        }

        void UpdateAnimations(List<Movement.PlayerCollision> collisions)
        {
            var isGrounded = Movement.IsGrounded(collisions);
            var velocity = this.movementBehaviour.Velocity;
            var isMovingHorizontally = Math.Abs(velocity.x) > 0.01f;
            Debug.Log(velocity.x);
            var isFacingRight = velocity.x > 0f;
            var isPushing = false;

            foreach (var collision in collisions)
            {
                isPushing = collision.tag == "DungBall" &&
                    (collision.direction == Vector2.left || collision.direction == Vector2.right);
            }

            this.SetInAir(!isGrounded);
            this.SetPushing(isPushing);

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
            this.playerAnimator.SetBool("IsPushing", isPushing);

        }
    }
}