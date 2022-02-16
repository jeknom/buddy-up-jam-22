using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Vector2 = UnityEngine.Vector2;

namespace Game
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class Movement : MonoBehaviour
    {
        public struct PlayerCollision
        {
            public string tag;
            public Vector2 direction;
        }

        public UnityEvent<List<PlayerCollision>> onUpdateCollisions;
        public UnityEvent onPlayerDestroy;
        public Vector2 Velocity => this.rb2d.velocity;

        [Header("Setup")]
        [SerializeField] LayerMask groundLayers;

        [Header("Modifiers")]
        [SerializeField, Range(1, 500)] float horizontalAcceleration = 150f;
        [SerializeField, Range(1, 500)] float horizontalMaxSpeed = 15f;
        [SerializeField, Range(1, 100)] float jumpForce = 4.5f;
        [SerializeField, Range(0, 1f)] float slide = 0.5f;
        [SerializeField, Range(0, 1f)] float movementSmoothing = 0.01f;
        [SerializeField, Range(0.1f, 1f)] float collisionBoxLength = 0.1f;
        [SerializeField, Range(0.01f, 1f)] float collisionBoxSize = 0.90f;
        [SerializeField, Range(0.01f, 1f)] float airMovementRatio = 0.4f;
        [SerializeField] bool allowAirControl;

        [Header("Other")]
        [SerializeField] bool debug;
        [SerializeField] Rect debugPos = new Rect(Vector2.zero, new Vector2(100f, 50f));

        Rigidbody2D rb2d;
        Collider2D collider2d;
        Vector2 currentVelocity = Vector2.zero;
        List<PlayerCollision> currentCollisions;

        public static bool IsGrounded(List<PlayerCollision> collisions)
        {
            foreach (var collision in collisions)
            {
                if (collision.direction == Vector2.down)
                {
                    return true;
                }
            }

            return false;
        }

        void Start()
        {
            this.rb2d = this.GetComponent<Rigidbody2D>();
            this.collider2d = this.GetComponent<Collider2D>();

            if (this.groundLayers == 0)
            {
                Debug.LogWarning(
                    $"{nameof(Movement)}: It seems that the layer mask for the ground layer has not been set. Jumping might not work!");
            }
        }

        public void Move(Vector2 dir, bool isJumping, bool isSprinting)
        {
            this.currentCollisions = this.GetCollisions();
            onUpdateCollisions.Invoke(this.currentCollisions);
            var isGrounded = IsGrounded(this.currentCollisions);

            if (!this.allowAirControl && !isGrounded)
            {
                return;
            }

            if (isJumping && isGrounded)
            {
                this.HandleJump();
            }

            this.HandleHorizontalMovement(dir, !isGrounded);
        }

        void HandleHorizontalMovement(Vector2 dir, bool isInAir)
        {
            var velocity = this.rb2d.velocity;

            var result = velocity.x;

            if (dir == Vector2.left && result > -this.horizontalMaxSpeed)
            {
                result -= this.horizontalAcceleration * Time.deltaTime;
            }
            else if (dir == Vector2.left)
            {
                result = -this.horizontalMaxSpeed;
            }
            else if (dir == Vector2.right && result < this.horizontalMaxSpeed)
            {
                result += this.horizontalAcceleration * Time.deltaTime;
            }
            else if (dir == Vector2.right)
            {
                result = this.horizontalMaxSpeed;
            }
            else
            {
                result *= this.slide;
            }

            if (isInAir)
            {
                result *= this.airMovementRatio;
            }

            this.rb2d.velocity = Vector2.SmoothDamp(
                current: velocity,
                target: new Vector2(result, velocity.y),
                currentVelocity: ref this.currentVelocity,
                smoothTime: this.movementSmoothing);
        }

        void HandleJump()
        {
            this.rb2d.AddForce(new Vector2(0f, this.jumpForce * this.rb2d.gravityScale), ForceMode2D.Impulse);
        }

        List<PlayerCollision> GetCollisions()
        {
            var collisions = new List<PlayerCollision>();
            var left = this.GetHit(Vector2.left, this.groundLayers);
            if (left.collider != null)
            {
                collisions.Add(new PlayerCollision
                {
                    tag = left.collider.tag,
                    direction = Vector2.left
                });
            }

            var right = this.GetHit(Vector2.right, this.groundLayers);
            if (right.collider != null)
            {
                collisions.Add(new PlayerCollision
                {
                    tag = right.collider.tag,
                    direction = Vector2.right
                });
            }

            var down = this.GetHit(Vector2.down, this.groundLayers);
            if (down.collider != null)
            {
                collisions.Add(new PlayerCollision
                {
                    tag = down.collider.tag,
                    direction = Vector2.down
                });
            }

            return collisions;
        }

        RaycastHit2D GetHit(Vector2 dir, LayerMask mask)
        {
            var playerBounds = this.collider2d.bounds;
            var hit = Physics2D.BoxCast(
                origin: playerBounds.center,
                size: playerBounds.size * this.collisionBoxSize,
                angle: 0f,
                direction: dir,
                distance: this.collisionBoxLength,
                layerMask: mask);

            return hit;
        }

        void OnGUI()
        {
            if (this.debug)
            {
                GUI.Label(this.debugPos, new GUIContent($"Is grounded: {IsGrounded(this.currentCollisions)}"));
            }
        }

        void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.gameObject.layer == 9)
            {
                this.onPlayerDestroy.Invoke();
                Destroy(this.gameObject);
            }
        }
    }
}