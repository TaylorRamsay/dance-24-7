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

    public static Collider[] targetDetector;
    public Transform targetCheck;
    public Transform attackCheck;
    public float checkDistance;
    public LayerMask checkLayer;

    public bool pursueTarget = false;
    public bool agro = false;
    public bool targeted = false;
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
            
            agro = false;
            playerMovement.agroEnemies.Remove(gameObject.GetComponent<EnemyNPC>());

            if(!playerMovement.agroEnemies.Contains(gameObject.GetComponent<EnemyNPC>()))
            {
                gameObject.GetComponent<MeshRenderer>().enabled = false;
            }
            
        }
    }

    void Attack()
    {
        attackDetector = Physics.OverlapSphere(attackCheck.position, attackDistance, checkLayer);

        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f && attackDetector.Length > 0)
        {
            player.GetComponent<StatManager>().ReceiveDamage(stats.attackPower);
                attackTimer = attackTime;
        }
    }


    void CheckForTarget()
    {
        targetDetector = Physics.OverlapSphere(targetCheck.position, checkDistance, checkLayer);
        if (targetDetector.Length > 0 && stats.hp > 0)
        {
            if (!playerMovement.agroEnemies.Contains(gameObject.GetComponent<EnemyNPC>()))
            {
                playerMovement.agroEnemies.Add(gameObject.GetComponent<EnemyNPC>());
            }

            pursueTarget = true;
            agro = true;
            agroIdentifier.SetActive(true);
            transform.LookAt(player.transform);
            navAgent.destination = player.transform.position;

        } else
        {
            playerMovement.agroEnemies.Remove(gameObject.GetComponent<EnemyNPC>());
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
        CheckForTarget();
        Attack();

    }
}
