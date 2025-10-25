using UnityEngine;

public class Objective : MonoBehaviour
{
    public GameManager gm;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision && collision.gameObject.CompareTag("Player")){
           // Destroy(collision.gameObject);
            gm.WinGame();}
    }
}
