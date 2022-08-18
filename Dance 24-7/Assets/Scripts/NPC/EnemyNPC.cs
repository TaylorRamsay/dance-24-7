using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNPC : MonoBehaviour
{
    public GameObject player;
    public ThirdPersonMovement playerMovement;
    public NavMeshAgent navAgent;
    public StatManager stats;
    public GameObject agroIdentifier;

    public EnemyNavMesh navManager;

    public static Collider[] attackDetector;
    public float movementSpeed;


    // Combat Variables
    public bool combatState = false;
    public bool attackFlag = false;
    public static Collider[] targetDetector;
    public float attackTime;
    public float attackTimer;
    public Transform targetCheck;
    public Transform attackCheck;
    public float checkDistance;
    public LayerMask checkLayer;
    public bool isTargeting = false;
    public bool targeted = false;
    public NPC combatTarget;
    public ThirdPersonMovement targetPlayer;
    public bool targetingPlayer = false;

    public float attackDistance;
    public GameObject weapon;

    public bool pursueTarget = false;
    public bool agro = false;


    void HealthCheck()
    {
        if (stats.hp <= 0)
        {
            
            agro = false;
            playerMovement.agroEnemies.Remove(gameObject.GetComponent<EnemyNPC>());

            if(!playerMovement.agroEnemies.Contains(gameObject.GetComponent<EnemyNPC>()))
            {
                //gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.SetActive(false);
            }
            
        }
    }

    void AttackTarget()
    {
        if (combatState)
        {
            if (/*(playerMovement.bandMembers.Count != 0) &&*/ attackTimer <= 0f)
            {
                navManager.NPCTargeting(this);
                attackFlag = true;
                attackTimer = attackTime;
            }
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
            combatState = true;
            agroIdentifier.SetActive(true);
            //transform.LookAt(player.transform);
            //navAgent.destination = player.transform.position;

        } else
        {
            combatState = false;
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
        AttackTarget();
        if (isTargeting && playerMovement.bandMembers.Contains(combatTarget))
        {
            navAgent.destination = combatTarget.transform.position;
        }
        attackTimer -= Time.deltaTime;

    }
}
