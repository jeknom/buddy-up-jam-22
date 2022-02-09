using UnityEngine;
using MovementTemplates.Scripts;

namespace Game
{
    [RequireComponent(typeof(Movement), typeof(PlayerAnimation))]
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] KeyCode jumpKey;
        Movement movementBehaviour;
        PlayerAnimation playerAnimation;

        void Awake()
        {
            this.movementBehaviour = this.GetComponent<Movement>();
            this.playerAnimation = this.GetComponent<PlayerAnimation>();
        }

        void Update()
        {
            var horizontalAxis = Input.GetAxisRaw("Horizontal");
            var direction = horizontalAxis > 0 ?
                Movement.Direction.Right :
                horizontalAxis < 0 ?
                Movement.Direction.Left :
                Movement.Direction.None;
            var isJumping = Input.GetKeyDown(this.jumpKey);
            var isGrounded = this.movementBehaviour.IsGrounded;

            this.playerAnimation.SetInAir(!isGrounded);
            if (direction != Movement.Direction.None)
            {
                this.playerAnimation.SetRunning(direction == Movement.Direction.Right);
            }
            else
            {
                this.playerAnimation.SetIdle();
            }

            this.movementBehaviour.Move(direction, isJumping, false);
        }
    }
}