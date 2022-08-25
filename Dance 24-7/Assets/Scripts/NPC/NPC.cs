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
    public GameObject npcDirection;
    public bool isFollowing = false;
    public GameObject followIdentifier;

    // Combat Variables
    public bool combatState = false;
    public bool attackFlag = false;
    public float attackTime;
    public float attackTimer = 0;
    public bool isTargeting = false;
    public bool targeted = false;
    public EnemyNPC combatTarget;

    public GameObject weapon;

    void HealthCheck()
    {
        if (stats.hp <= 0)
        {
            playerMovement.bandMembers.Remove(gameObject.GetComponent<NPC>());

            if (!playerMovement.bandMembers.Contains(gameObject.GetComponent<NPC>()))
            {
                navAgent.enabled = false;
                combatState = false;
                stats.attackPower = 0;
                gameObject.GetComponent<MeshRenderer>().enabled = false;
                gameObject.GetComponent<BoxCollider>().enabled = false;
                npcDirection.SetActive(false);
                followIdentifier.GetComponent<MeshRenderer>().enabled = false;
                weapon.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                
                //weapon.SetActive(false);
            }
        }
    }

    void AttackTarget()
    {
        if (combatState)
        {
            isFollowing = false;
            //followIdentifier.SetActive(false);
            if ((playerMovement.agroEnemies.Count != 0) && attackTimer <= 0f)
            {
                playerMovement.GetComponent<NPCNavMesh>().EnemyTargeting(this);
                attackFlag = true;
            }
        } else
        {
            attackFlag = false;
        }
    }

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.stoppingDistance = 4;
        followIdentifier.SetActive(false);
    }

    void Update()
    {
        HealthCheck();
        AttackTarget();
        attackTimer -= Time.deltaTime;
        if (combatState)
        {
            weapon.GetComponent<Weapon>().SwingWeapon();
        }

        if (isFollowing)
        {
            followIdentifier.SetActive(true);
            navAgent.speed = playerMovement.speed;
        }

        if (isTargeting && navAgent.enabled == true)
        {
            navAgent.destination = combatTarget.transform.position;
        }
    }
}
