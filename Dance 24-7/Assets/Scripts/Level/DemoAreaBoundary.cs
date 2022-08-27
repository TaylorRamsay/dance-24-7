using UnityEngine;

public class DemoAreaBoundary : MonoBehaviour
{
    public ThirdPersonMovement playerManager;
    public NPC neededNPC;
    public GameObject boundary;

    void Start()
    {
        boundary.SetActive(true);
    }

    // Used to disable temporary boundary once objective is completed, in this case, when a specific NPC is added to bandMembers list
    void Update()
    {
        if (playerManager.bandMembers.Contains(neededNPC))
        {
            boundary.SetActive(false);
        }
    }
}
