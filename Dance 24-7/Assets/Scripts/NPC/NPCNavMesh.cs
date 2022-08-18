using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCNavMesh : MonoBehaviour
{

    //private NavMeshAgent navMeshAgent;
    public GameObject player;
    public ThirdPersonMovement playerManager;
    public CombatManager combat;

    void PlayerFollower(NPC firstBandMember)
    {
        firstBandMember.gameObject.GetComponent<NPC>().navAgent.destination = player.transform.position;
    }

    void Follow()
    {
        if (!combat.activeCombat)
        {
            for (int i = 0; i < playerManager.bandMembers.Count; i++)
            {
                if (playerManager.bandMembers[i].isFollowing)
                {
                    if (i == 0)
                    {
                        PlayerFollower(playerManager.bandMembers[i]);
                    }
                    else
                    {
                        playerManager.bandMembers[i].navAgent.destination = playerManager.bandMembers[i - 1].transform.position;
                    }
                }
            }
        }
    }

    public void EnemyTargeting(NPC attacker)
    {
        if (combat.activeCombat)
        {
            if (attacker.isTargeting == false)
            {
                for (int i = 0; i < playerManager.agroEnemies.Count; i++)
                {
                    if (playerManager.agroEnemies[i].GetComponent<EnemyNPC>().targeted == false)
                    {
                        attacker.combatTarget = playerManager.agroEnemies[i];
                        attacker.isTargeting = true;
                        playerManager.agroEnemies[i].GetComponent<EnemyNPC>().targeted = true;

                        //print(attacker + " is targeting " + attacker.combatTarget);

                        goto after;
                    } else if (i == playerManager.agroEnemies.Count - 1)
                    {
                        attacker.combatTarget = playerManager.agroEnemies[0];
                        attacker.isTargeting = true;
                        goto after;
                    }
                }
            } else
            {
                attacker.gameObject.transform.LookAt(attacker.combatTarget.transform);
            }
        after:;
        }
        if (attacker.combatTarget.GetComponent<StatManager>().IsDefeated())
        {
            attacker.isTargeting = false;
        }
    }

    private void Update()
    {
        Follow();
        //EnemyTargeting();
    }
}
