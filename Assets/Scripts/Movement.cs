using UnityEngine;
using Vector2 = UnityEngine.Vector2;

namespace MovementTemplates.Scripts
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class Movement : MonoBehaviour
    {
        public enum Direction
        {
            None,
            Left,
            Right,
        }

        [Header("Setup")]
        [SerializeField] LayerMask groundLayer;
        [SerializeField] LayerMask dungBallLayer;

        [Header("Modifiers")]
        [SerializeField, Range(1, 500)] float horizontalAcceleration = 150f;
        [SerializeField, Range(1, 500)] float horizontalMaxSpeed = 15f;
        [SerializeField, Range(1, 500)] float horizontalMoveFastMaxSpeed = 20f;
        [SerializeField, Range(1, 500)] float horizontalMoveSlowMaxSpeed = 5f;
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
        Direction lastDirection = Direction.None;
        bool isMovingFast;
        bool isMovingSlow;

        void Start()
        {
            this.rb2d = this.GetComponent<Rigidbody2D>();
            this.collider2d = this.GetComponent<Collider2D>();

            if (this.groundLayer.value == 0)
            {
                Debug.LogWarning(
                    $"{nameof(Movement)}: It seems that the layer mask for the ground layer has not been set. Jumping might not work!");
            }

            if (this.dungBallLayer.value == 0)
            {
                Debug.LogWarning(
                    $"{nameof(Movement)}: It seems that the layer mask for the dung ball has not been set. Jumping on top of it might not work!");
            }
        }

        public void Move(Direction dir, bool isJumping, bool isSprinting)
        {
            var isGrounded = this.IsColliding(Vector2.down);

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

        void HandleHorizontalMovement(Direction dir, bool isInAir)
        {
            var velocity = this.rb2d.velocity;
            var switchedDirection = (this.lastDirection == Direction.Left && velocity.x > 0f) ||
                                    (this.lastDirection == Direction.Right && velocity.x < 0f);

            var result = switchedDirection ? 0f : velocity.x;
            var maxSpeedToUse = this.isMovingFast ?
                this.horizontalMoveFastMaxSpeed :
                this.isMovingSlow ?
                this.horizontalMoveSlowMaxSpeed :
                this.horizontalMaxSpeed;

            if (dir == Direction.Left && result > -maxSpeedToUse)
            {
                result -= this.horizontalAcceleration * Time.deltaTime;
            }
            else if (dir == Direction.Left)
            {
                result = -maxSpeedToUse;
            }
            else if (dir == Direction.Right && result < maxSpeedToUse)
            {
                result += this.horizontalAcceleration * Time.deltaTime;
            }
            else if (dir == Direction.Right)
            {
                result = maxSpeedToUse;
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

        bool IsColliding(Vector2 direction)
        {
            var playerBounds = this.collider2d.bounds;
            var hit = Physics2D.BoxCast(
                origin: playerBounds.center,
                size: playerBounds.size * this.collisionBoxSize,
                angle: 0f,
                direction: direction,
                distance: this.collisionBoxLength,
                layerMask: this.groundLayer);

            var isHit = hit.collider != null;

            if (!isHit)
            {
                hit = Physics2D.BoxCast(
                    origin: playerBounds.center,
                    size: playerBounds.size * this.collisionBoxSize,
                    angle: 0f,
                    direction: direction,
                    distance: this.collisionBoxLength,
                    layerMask: this.dungBallLayer);

                isHit = hit.collider != null;
            }

            return isHit;
        }

        void OnGUI()
        {
            if (this.debug)
            {
                GUI.Label(this.debugPos, new GUIContent($"Is grounded: {this.IsColliding(Vector2.down)}"));
            }
        }
    }
}