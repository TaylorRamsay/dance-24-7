using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public GameObject player;
    public ThirdPersonMovement playerMovement;
    public StatManager stats;
    public NavMeshAgent navAgent;
    public GameObject npc;
    public bool isFollowing = false;
    public GameObject followIdentifier;

    public bool combatState = false;
    public static Collider[] enemyDetector;
    public float attackTime;
    public float attackTimer = 0;
    public Transform attackCheck;
    public LayerMask checkLayer;

    public float attackDistance;
    public GameObject weapon;

    void AttackTarget()
    {
        if (combatState)
        {
            isFollowing = false;
            //followIdentifier.SetActive(false);
            //enemyDetector = Physics.OverlapSphere(attackCheck.position, attackDistance, checkLayer);

            if ((playerMovement.agroEnemies.Count != 0) && attackTimer <= 0f /*&& enemyDetector.Length > 0*/)
            {
                
                attackTimer = attackTime;
            }
        }
    }

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.stoppingDistance = 5;
        followIdentifier.SetActive(false);
    }

    void Update()
    {
        AttackTarget();
        if (isFollowing)
        {
            followIdentifier.SetActive(true);
            navAgent.speed = playerMovement.speed;
        }
    }
}
