using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowManager : MonoBehaviour
{

    public GameObject player;
    public ThirdPersonMovement personMovement;
    public List<object> followers;
    public int followersSize;
    public float targetDistance;
    public float maxDistance = 3;
    public float followSpeed;
    public RaycastHit search;


    void Follow()
    {
        // special case - first npc needs to follow player
        for (int i = 1; i < followersSize; i++)
        {
            // each npc follows the one located before it in the list, except for npc that follows player
        }
    }

    void Start()
    {
        // Populate followers List

    }

    void Update()
    {
        
    }
}
