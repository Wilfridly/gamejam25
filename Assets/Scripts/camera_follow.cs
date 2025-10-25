using UnityEngine;

public class camera_follow : MonoBehaviour
{

    public GameObject player;
    public float timeOffset;  // decalage dans le temps
    public Vector3 posOffset; 
    
    private Vector3 velocity;
   

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + posOffset, ref velocity, timeOffset);     
    }
}
