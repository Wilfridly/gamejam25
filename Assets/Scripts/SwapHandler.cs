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
    private GameObject FindClosestNPC(Vector3 playerPos) {
        GameObject[] npcs = GameObject.FindGameObjectsWithTag("Swappable");
        Debug.LogError($"Found {npcs.Length} NPCs with tag 'Swappable'");
        if (npcs.Length == 0)
            throw new System.Exception("No NPCs with tag 'Swappable' found in the scene.");
        float shortestDistance = Mathf.Infinity;
        GameObject closest = null;
        foreach (GameObject npc in npcs)
        {
            Debug.LogError($"Checking NPC at position {npc.transform.position}");
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
            npc = FindClosestNPC(playerPos);
        } catch (System.Exception e) {
            Debug.LogError(e.Message);
            //Todo add visuals if no swap possible?
            return;
        }
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
