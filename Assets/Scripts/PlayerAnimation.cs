using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public class PlayerAnimation : MonoBehaviour
    {
        Animator playerAnimator;
        SpriteRenderer playerRenderer;
        bool isFacingRight = true;

        void Awake()
        {
            this.playerAnimator = this.GetComponent<Animator>();
            this.playerRenderer = this.GetComponent<SpriteRenderer>();
        }

    
        public void SetRunning(bool isFacingRight)
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

        public void SetIdle()
        {
            if (!this.playerAnimator.GetBool("IsRunning"))
            {
                return;
            }

            this.playerAnimator.SetBool("IsRunning", false);
        }

        public void SetInAir(bool isInAir)
        {
            if (isInAir == this.playerAnimator.GetBool("IsInAir"))
            {
                return;
            }

            this.playerAnimator.SetBool("IsInAir", isInAir);
        }
    }
}