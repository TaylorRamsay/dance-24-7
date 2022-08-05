using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNPC : MonoBehaviour
{
    public GameObject player;
    public ThirdPersonMovement playerMovement;
    public NavMeshAgent navAgent;
    public GameObject npc;
    public StatManager stats;

    public static Collider[] playerDetector;
    public Transform playerCheck;
    public Transform attackCheck;
    public float checkDistance;
    public LayerMask checkLayer;

    public bool pursueTarget = false;
    public bool agro = false;
    public GameObject agroIdentifier;
    public static Collider[] attackDetector;
    public float attackTime;
    private float attackTimer;

    public float attackDistance;
    public float movementSpeed;

    void HealthCheck()
    {
        if (stats.hp <= 0)
        {
            playerMovement.agroEnemies.Remove(this);
            gameObject.SetActive(false);
        }
    }

    void Attack()
    {
        attackDetector = Physics.OverlapSphere(attackCheck.position, attackDistance, checkLayer);

        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f && attackDetector.Length > 0)
        { 
                player.GetComponent<StatManager>().hp -= stats.attackPower;
                attackTimer = attackTime;
        }
    }


    void CheckForPlayer()
    {
        playerDetector = Physics.OverlapSphere(playerCheck.position, checkDistance, checkLayer);
        if (playerDetector.Length > 0)
        {
            if (!playerMovement.agroEnemies.Contains(this))
            {
                playerMovement.agroEnemies.Add(this);
            }
            pursueTarget = true;
            agro = true;
            agroIdentifier.SetActive(true);
            transform.LookAt(player.transform);
            navAgent.destination = player.transform.position;

        } else
        {
            playerMovement.agroEnemies.Remove(this);
            pursueTarget = false;
            agro = false;
            agroIdentifier.SetActive(false);
        }
    }

    void Start()
    {
        agroIdentifier.SetActive(false);
        navAgent.speed = movementSpeed;
        navAgent.stoppingDistance = 5;
    }

    void Update()
    {
        HealthCheck();
        CheckForPlayer();
        Attack();

    }
}
