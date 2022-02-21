using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    [RequireComponent(typeof(Movement))]
    public class PlayerInput : MonoBehaviour
    {
        [Header("Setup")]
        public List<KeyCode> jumpKeys;

        public UnityEvent onChangeDirection;

        Movement movementBehaviour;
        bool isDead;
        Vector2 lastDirection = Vector2.right;

        float horizontalAxis;
        bool isJumping;

        void Awake()
        {
            this.movementBehaviour = this.GetComponent<Movement>();
            this.movementBehaviour.onPlayerDestroy.AddListener(() => this.isDead = true);
        }

        void Update()
        {
            this.horizontalAxis = Input.GetAxisRaw("Horizontal");

            foreach (var key in this.jumpKeys)
            {
                var isPressed = Input.GetKeyDown(key);
                if (isPressed && !this.isJumping)
                {
                    isJumping = true;
                    break;
                }
            }
        }

        void FixedUpdate()
        {
            if (this.isDead)
            {
                return;
            }

            var direction = horizontalAxis > 0 ? Vector2.right : horizontalAxis < 0 ? Vector2.left : Vector2.zero;
            if (direction != this.lastDirection && direction != Vector2.zero)
            {
                onChangeDirection.Invoke();
                this.lastDirection = direction;
            }

            this.movementBehaviour.Move(direction, isJumping, false);

            if (this.isJumping)
            {
                this.isJumping = false;
            }
        }
    }
}