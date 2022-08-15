using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCNavMesh : MonoBehaviour
{

    //private NavMeshAgent navMeshAgent;
    public GameObject player;
    public ThirdPersonMovement playerMovement;
    public CombatManager combat;

    void PlayerFollower(NPC firstBandMember)
    {
        firstBandMember.gameObject.GetComponent<NPC>().navAgent.destination = player.transform.position;
    }

    void Follow()
    {
        if (!combat.activeCombat)
        {
            for (int i = 0; i < playerMovement.bandMembers.Count; i++)
            {
                if (playerMovement.bandMembers[i].isFollowing)
                {
                    if (i == 0)
                    {
                        PlayerFollower(playerMovement.bandMembers[i]);
                    }
                    else
                    {
                        playerMovement.bandMembers[i].navAgent.destination = playerMovement.bandMembers[i - 1].transform.position;
                    }
                }
            }
        }
    }

    void EnemyFollower()
    {
        if (combat.activeCombat)
        {
            for (int i = 0; i < playerMovement.bandMembers.Count; i++)
            { 
                if (playerMovement.agroEnemies.Count != 0)
                {
                    playerMovement.bandMembers[i].navAgent.destination = playerMovement.agroEnemies[0].transform.position;
                }
                
                
                
            }
        }
    }

    private void Update()
    {
        Follow();
        EnemyFollower();
    }
}
