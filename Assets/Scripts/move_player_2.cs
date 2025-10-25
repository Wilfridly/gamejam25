using UnityEngine;

public class move_player_numeric : MonoBehaviour
{
    public float move_speed = 500f;
    public Rigidbody2D Rb;      
    public Animator animator;   
    public SpriteRenderer SpriteRenderer; 
    
    private Vector3 velocity = Vector3.zero; 

    void Update()
    {
        // Movement Computation using numeric keys
        float horizontalMovement = 0f;
        float verticalMovement = 0f;

        if (Input.GetKey(KeyCode.Keypad6)) horizontalMovement = 1f * move_speed * Time.deltaTime; // Right
        if (Input.GetKey(KeyCode.Keypad4)) horizontalMovement = -1f * move_speed * Time.deltaTime; // Left
        if (Input.GetKey(KeyCode.Keypad8)) verticalMovement = 1f * move_speed * Time.deltaTime; // Up
        if (Input.GetKey(KeyCode.Keypad2)) verticalMovement = -1f * move_speed * Time.deltaTime; // Down

        // Apply Movement
        Move(horizontalMovement, verticalMovement);

        // Update orientation
        flip(Rb.linearVelocity.x);

        // Update animator
        UpdateAnimator(horizontalMovement, verticalMovement);
    }

    void Move(float _horizontalMovement, float _verticalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, _verticalMovement);
        Rb.linearVelocity = Vector3.SmoothDamp(Rb.linearVelocity, targetVelocity, ref velocity, 0.05f);
    }

    void flip(float _velocity)
    {
        if (_velocity > 0.1f)
            SpriteRenderer.flipX = false;
        else if (_velocity < -0.1f)
            SpriteRenderer.flipX = true;
    }

    void UpdateAnimator(float horizontal, float vertical)
    {
        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
        {
            animator.SetInteger("direction", 2); // Side
        }
        else if (vertical > 0.1f)
        {
            animator.SetInteger("direction", 1); // Up
        }
        else if (vertical < -0.1f)
        {
            animator.SetInteger("direction", 0); // Down
        }

        float speed = new Vector2(horizontal, vertical).magnitude;
        animator.SetFloat("speed", speed);
    }
}
