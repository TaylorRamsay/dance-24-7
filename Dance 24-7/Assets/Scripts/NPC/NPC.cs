using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public GameObject player;
    public ThirdPersonMovement playerMovement;
    public NavMeshAgent navAgent;
    public GameObject npc;
    public bool isFollowing = false;
    public GameObject followIdentifier;

    // Stats
    public float hp = 20;
    public float mp = 20;
    public float attackPower = 10;


    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.stoppingDistance = 5;
        followIdentifier.SetActive(false);
    }

    void Update()
    {
        if (isFollowing)
        {
            followIdentifier.SetActive(true);
            navAgent.speed = playerMovement.speed;
        }
    }
}
