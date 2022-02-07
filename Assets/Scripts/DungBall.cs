using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(Rigidbody2D))]
public class DungBall : MonoBehaviour
{
    public const string GROWER_TAG = "Grower";
    public const string SHRINKER_TAG = "Shrinker";

    [Header("Setup")]
    [SerializeField] LayerMask groundLayer;

    [Header("Modifiers")]
    [SerializeField, Range(1f, 10f)] float maxSize = 10f;
    [SerializeField, Range(1f, 10f)] float minSize = 1f;
    [SerializeField, Range(0.01f, 1f)] float collisionRadiusSize = 0.90f;
    [SerializeField, Range(0.1f, 1f)] float scalingModifier = 0.5f;

    [Header("Other")]
    [SerializeField] bool debug;
    [SerializeField] Rect debugPos = new Rect(new Vector2(0f, 0f), new Vector2(100f, 50f));

    CircleCollider2D collider2d;
    Rigidbody2D rb2d;
    
    void Awake()
    {
        this.collider2d = this.GetComponent<CircleCollider2D>();
        this.rb2d = this.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        var groundHit = this.GetCollision();
        if (groundHit.collider == null)
        {
            return;
        }

        
        var verticalVelocity = Mathf.Abs(this.rb2d.velocity.x);
        if (verticalVelocity <= 0f)
        {
            return;
        }

        var hitTag = groundHit.collider.tag;
        var sizeModifier = verticalVelocity * Time.deltaTime * this.scalingModifier;
        if (hitTag == SHRINKER_TAG)
        {
            sizeModifier = -sizeModifier;
        }

        var currentScale = this.transform.localScale.x;
        if ((sizeModifier > 0f && currentScale >= this.maxSize) ||
            (sizeModifier < 0f && currentScale <= this.minSize)) {
            return;
        }

        var sizeChange = new Vector3(sizeModifier, sizeModifier, 1f);
        this.transform.localScale += sizeChange;
    }

    RaycastHit2D GetCollision()
    {
        var playerBounds = this.collider2d.bounds;
        var hit = Physics2D.CircleCast(
            origin: playerBounds.center,
            radius: this.collider2d.radius + this.collisionRadiusSize,
            direction: Vector2.one,
            distance: 1f,
            layerMask: this.groundLayer);

        return hit;
    }

    void OnGUI()
    {
        if (this.debug)
        {
            var groundHit = this.GetCollision();
            var tagName = groundHit.collider != null ? groundHit.collider.tag : "None";
            var behaviourName = groundHit.collider != null ? groundHit.collider.name : "None";
            var tagText = $"Hit tag: {tagName}";
            var behaviourText = $"Hit object name: {behaviourName}";
            var secondPos = GetLabelNextLine(this.debugPos);
            var thirdPos = GetLabelNextLine(secondPos);
            var fourthPos = GetLabelNextLine(thirdPos);
            var fifthPos = GetLabelNextLine(fourthPos);

            GUI.Label(this.debugPos, new GUIContent(tagText));
            GUI.Label(secondPos, new GUIContent(behaviourText));
            GUI.Label(thirdPos, new GUIContent($"Min scale {this.minSize}"));
            GUI.Label(fourthPos, new GUIContent($"Max scale {this.maxSize}"));
            GUI.Label(fifthPos, new GUIContent($"Local scale {this.transform.localScale.x}"));
        }    
    }

    static Rect GetLabelNextLine(Rect prev)
    {
        var rect = new Rect(new Vector2(0f, prev.position.y + 50f), prev.size);

        return rect;
    }
}
