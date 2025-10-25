using UnityEngine;

public class SwapHandler : MonoBehaviour
{
    //Currently assigned via the Unity GUI
    public GameObject player;
    
    public SpriteRenderer playerRender;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Get player & NPC sprites
        playerRender = player.GetComponent<SpriteRenderer>();
    }
    private GameObject FindClosestNPCPosition(Vector3 playerPos) {
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("Swappable");
        if (npcs.Length == 0)
            throw new System.Exception("No NPCs with tag 'Swappable' found in the scene.");
        float shortestDistance = float.MaxValue;
        GameObject closest = null;
        foreach (GameObject npc in npcs)
        {
            float distance = Vector3.Distance(playerPos, npc.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closest = npc;
            }
        }
        return closest;
    }

    public void SwapCharacters() {
        // Get player & NPC position
        Vector3 playerPos = player.transform.position;

        GameObject npc; 
        try {
            npc = FindClosestNPCPosition(playerPos);
        } catch (System.Exception e) {
            Debug.LogError(e.Message);
            //Todo add visuals if no swap possible?
            return;
        }
        SpriteRenderer npcRender = npc.GetComponent<SpriteRenderer>();
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
