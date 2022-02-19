using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    [RequireComponent(typeof(Movement))]
    public class PlayerInput : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField] KeyCode jumpKey;

        public UnityEvent onChangeDirection;

        Movement movementBehaviour;
        bool isDead;
        Vector2 lastDirection = Vector2.right;

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
            if (direction != this.lastDirection && direction != Vector2.zero)
            {
                onChangeDirection.Invoke();
                this.lastDirection = direction;
            }
            var isJumping = Input.GetKeyDown(this.jumpKey);

            this.movementBehaviour.Move(direction, isJumping, false);
        }
    }
}