using UnityEngine;

public class Enemy_Patrol : MonoBehaviour
{
    public float move_speed ;
    public Transform[] wayPoints ;
    
    private Transform target;  // Destination Point "vector"
    private int destPoint = 0; // Destination index in the waypoints array
    
    public SpriteRenderer graphic; 
    
    // mode chasse du joueur
    public bool is_aware        = false;
    
    public int switches      = 3;
    public bool is_agressive = false;
   
    
    private Transform player;                 // Drag & drop dans l'Inspector
    public string playerTag = "Player";      // ou trouve par Tag
    
    public Rigidbody2D Rb;      // hitbox object
    public Animator animator;   // animator object
    
    public GameManager gm;
    
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
   
   void Start()
    {
        target = wayPoints[0]; // first destination point is the first waypoint
        
    }
    
    void UpdateAnimator(float horizontal, float vertical)
    {
        // Déterminer l'état selon la direction dominante
        if (Mathf.Abs(horizontal) > Mathf.Abs(vertical))
        {
            // Côté
            animator.SetInteger("direction", 2);
        }
        else if (vertical > 0.1f)
        {
            // Haut
            animator.SetInteger("direction", 1);
        }
        else if (vertical < -0.1f)
        {
            // Bas
            animator.SetInteger("direction", 0);
        }
        
        // Optionnel : vitesse pour blend
        float speed = new Vector2(horizontal, vertical).magnitude;
        animator.SetFloat("speed", speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (switches <= 0 )
        {
        is_agressive = true;
        
        }
    
        // Direction computation
        Vector3 dir = target.position - transform.position;
        
        // Movement Application
        transform.Translate(dir.normalized * move_speed * Time.deltaTime, Space.World);
        
        UpdateAnimator(
            dir.normalized.x,
            dir.normalized.y    
        );
        
        
        if (!is_agressive) // normal patern computation and application
        {
            // Distance to the target point computation in order to switch to the
            // next waypoint if required 
            if(Vector3.Distance(transform.position, target.position) < 0.3f)
            {
                
                // destPoint is an ID of a wayPoint, we want to goto the next one
                // the modulo is to do a loop in the wayPoints array.
                destPoint = (destPoint + 1) % wayPoints.Length;
                target = wayPoints[destPoint];
                
                // FlipX update using difference between current X coord and the 
                // new target Xcoord
                if (transform.position.x - target.position.x < 0) // if goto right
                {
                    graphic.flipX = false;
                }else if (transform.position.x - target.position.x > 0)// if goto left
                {
                    graphic.flipX = true;
                }
            }
        }
        else // mode chasse
        {
            move_speed   = 3;
            
            var p = GameObject.FindGameObjectWithTag("Player");// get the Player object
            if (p) player = p.transform;// get the Player position
            
            target = player;
            
            // FlipX update using difference between current X coord and the 
            // new target Xcoord
            if (transform.position.x - target.position.x < 0) // if goto right
            {
                graphic.flipX = false;
            }else if (transform.position.x - target.position.x > 0)// if goto left
            {
                graphic.flipX = true;
            }
        }
        
    }
    

    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null){
        
            // ================================================================== //
            // Effet de profondeur lors d'une collision avec un autre objet
            var otherSR = collision.GetComponentInChildren<SpriteRenderer>();
            if (otherSR == null || graphic == null) return;
            
            float delta_Y = collision.transform.position.y - transform.position.y;

            if (delta_Y < 0) {
                   // enemy passe derriere
                   graphic.sortingLayerID = otherSR.sortingLayerID;
                   graphic.sortingOrder   = otherSR.sortingOrder - 1;
                   
            } else {
                   // enemy passe devant
                   graphic.sortingLayerID = otherSR.sortingLayerID;
                   graphic.sortingOrder   = otherSR.sortingOrder + 1;
            }
            // ================================================================== //
            
            
            
            // si c'est le joueur qui entre en collision a vec l'object alors :
            if(collision.gameObject.CompareTag("Player"))
            {
               if(is_agressive)
               {
               // Destroy(collision.gameObject);
                gm.LoseGame();
               } 
               //move_speed   = 3; 
            }
        }
    


    }
    
    
    
    
    
    
    
    
    
    

    
    
}
