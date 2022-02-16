using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Movement))]
    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] KeyCode jumpKey;
        Movement movementBehaviour;
        bool isDead;

        void Awake()
        {
            this.movementBehaviour = this.GetComponent<Movement>();
            this.movementBehaviour.onPlayerDestroy.AddListener(() => this.isDead = true);
        }

        void Update()
        {
            if (this.isDead)
            {
                return;
            }

            var horizontalAxis = Input.GetAxisRaw("Horizontal");
            var direction = horizontalAxis > 0 ? Vector2.right : horizontalAxis < 0 ? Vector2.left : Vector2.zero;
            var isJumping = Input.GetKeyDown(this.jumpKey);

            this.movementBehaviour.Move(direction, isJumping, false);
        }
    }
}