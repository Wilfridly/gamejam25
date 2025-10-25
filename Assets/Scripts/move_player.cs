using UnityEngine;

public class move_player : MonoBehaviour
{

    public float move_speed ;
    public Rigidbody2D Rb;      // hitbox object
    public Animator animator;   // animator object
    public SpriteRenderer SpriteRenderer; 
    
    private Vector3 velocity = Vector3.zero; 
        
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   /*
   void Start()
    {
        
    }
    */
    // Update is called once per frame %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    void Update()
    {
        // Movement Computation
        float horizontalMovement = Input.GetAxis("Horizontal") * move_speed * Time.deltaTime;
        float verticalMovement   = Input.GetAxis("Vertical") * move_speed * Time.deltaTime; 
        
        // Movement Application to the Rigidbody -------------------------------
        Move(horizontalMovement, verticalMovement);
        
        // Using movement computation define character orientation
        flip(Rb.linearVelocity.x);
        
        // Links Movement Applicaion to the Animator ---------------------------
        // => due to x and y possible negatives negatives values :
        float character_velocity = Mathf.Abs(10*Rb.linearVelocity.x + Rb.linearVelocity.y );
        // => link to animator :
        animator.SetFloat("speed", character_velocity);
        
        
        
    } // %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
    
    
    
    
    
    
    
    
    
    // Movement Application to the RB ---------------------------------
    void Move( float _horizontalMovement, float _verticalMovement)
    {
        // New position computation (new X, new Y)
        Vector3 targetvelocity = new Vector2(_horizontalMovement, _verticalMovement);
        
        // Rigidbody position = New ??? (ActualPlace, DestinationPlace, ???, Echelle de tamps pour effectuer le deplacement)                               
        Rb.linearVelocity = Vector3.SmoothDamp(Rb.linearVelocity, targetvelocity, ref velocity, .05f);
    
    }
    
    
    
    // update graphic sense 
    void flip( float _velocity)
    {
    
        if (_velocity > 0.1f) 
        {
            SpriteRenderer.flipX = false;
        
        }else if (_velocity < -0.1f) 
        {
            SpriteRenderer.flipX = true;
        
        }
    
    }
    
    
    
    

    
    
    
    
}
























