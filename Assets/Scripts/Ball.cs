using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TopDownBallBounce2D : MonoBehaviour
{
    [Header("Bounce Settings")]
    public float minSpeed = 2f;      // Minimum speed to prevent sticking
    public float bounceMultiplier = 1f; // Can increase speed on bounce if desired
    public float jitterStrength = 0.01f; // Tiny random nudge to escape edges

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // Top-down → no gravity
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }

    void FixedUpdate()
    {
        // Keep a minimum speed to prevent sticking
        if (rb.linearVelocity.magnitude < minSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * minSpeed;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb.linearVelocity.magnitude == 0f)
            return;

        // Reflect velocity along collision normal
        Vector2 normal = collision.contacts[0].normal;
        Vector2 reflectDir = Vector2.Reflect(rb.linearVelocity, normal).normalized;

        // Apply bounce multiplier and maintain speed
        rb.linearVelocity = reflectDir * rb.linearVelocity.magnitude * bounceMultiplier;

        // Optional tiny random jitter to avoid sticking in corners
        rb.linearVelocity += Random.insideUnitCircle * jitterStrength;
    }
}
