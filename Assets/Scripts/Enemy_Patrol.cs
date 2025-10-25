using UnityEngine;

public class Enemy_Patrol : MonoBehaviour
{
    public float move_speed ;
    public Transform[] wayPoints ;
    
    private Transform target;  // Destination Point "vector"
    private int destPoint = 0; // Destination index in the waypoints array
    
    public SpriteRenderer graphic; 
    
    // mode chasse du joueur
    public bool is_agressive = false;
    
    private Transform player;                 // Drag & drop dans l'Inspector
    public string playerTag = "Player";      // ou trouve par Tag
    
    
    
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
   
   void Start()
    {
        target = wayPoints[0]; // first destination point is the first waypoint
        
    }
    

    // Update is called once per frame
    void Update()
    {
        
    
        // Direction computation
        Vector3 dir = target.position - transform.position;
        
        // Movement Application
        transform.Translate(dir.normalized * move_speed * Time.deltaTime, Space.World);
        
        
        
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
        // si c'est le joueur qui entre en collision avec l'object alors :
        if(collision.gameObject.CompareTag("Player"))
        {
           //is_agressive = true;
           //move_speed   = 3; 
        }
        
        
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

    }
    
    
    
    
    
    
    
    
    
    

    
    
}
