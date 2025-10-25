using UnityEngine;

public class SwapHandler : MonoBehaviour
{
    //Currently assigned via the Unity GUI
    public GameObject player;
    public GameObject npc;
    
    public SpriteRenderer playerRender;
    public SpriteRenderer npcRender;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Get player & NPC sprites
        playerRender = player.GetComponent<SpriteRenderer>();
        npcRender = npc.GetComponent<SpriteRenderer>();
    }

    public void SwapCharacters() {
        // Get player & NPC position
        Vector3 playerPos = player.transform.position;
        Vector3 npcPos = npc.transform.position;
        
        // Set new positions
        player.transform.position = npcPos;
        npc.transform.position = playerPos;
        
        
        // Get sprite components
        SpriteRenderer currentPlayerRender = player.GetComponent<SpriteRenderer>();
        SpriteRenderer currentNpcRender = npc.GetComponent<SpriteRenderer>();
        
        if (!playerRender || !npcRender) {
            Debug.LogError("No SpriteRenderer attached to the components!");
            return;
        }
        // Set new swap components
        playerRender = currentNpcRender;
        npcRender = currentPlayerRender;
        
        // Update animations
        Animator playerAnimator = player.GetComponent<Animator>();
        Animator npcAnimator = npc.GetComponent<Animator>();
        if (!playerAnimator || !npcAnimator) {
            Debug.LogError("No Animator attached to the components!");
            return;
        }
        
        RuntimeAnimatorController playerControl = playerAnimator.runtimeAnimatorController;
        RuntimeAnimatorController npcControl = npcAnimator.runtimeAnimatorController;
        
        playerAnimator.runtimeAnimatorController = npcControl;
        npcAnimator.runtimeAnimatorController = playerControl;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            SwapCharacters();
        
    }
}
