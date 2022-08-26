using System.Collections;
using System.Collections.Generic;
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

    void Update()
    {
        if (playerManager.bandMembers.Contains(neededNPC))
        {
            boundary.SetActive(false);
        }
    }
}
