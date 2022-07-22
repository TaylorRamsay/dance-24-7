using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public GameObject player;
    public ThirdPersonMovement playerMovement;
    public float speedModifier = 100;
    public GameObject npc;
    public float targetDistance;
    public float maxDistance = 3;
    public float followSpeed;
    public bool isFollowing = false;

    void Start()
    {
        
    }


    void Update()
    {

    }
}
