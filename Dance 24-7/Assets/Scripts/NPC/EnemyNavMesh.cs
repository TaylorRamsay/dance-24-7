using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNavMesh : MonoBehaviour
{

    //private NavMeshAgent navMeshAgent;
    public GameObject player;
    public ThirdPersonMovement playerManager;
    public CombatManager combat;

    public void Targeting(EnemyNPC attacker)
    {
        if (combat.activeCombat)
        {
            if (attacker.isTargeting == false && !attacker.targetingPlayer)
            {
                if (playerManager.bandMembers.Count == 0)
                {
                    attacker.targetPlayer = playerManager;
                    attacker.targetingPlayer = true;
                }
                for (int i = 0; i < playerManager.bandMembers.Count; i++)
                {
                    if (playerManager.bandMembers[i].GetComponent<NPC>().targeted == false)
                    {
                        attacker.combatTarget = playerManager.bandMembers[i];
                        attacker.isTargeting = true;
                        playerManager.bandMembers[i].GetComponent<NPC>().targeted = true;
                    }
                    else if (i == playerManager.bandMembers.Count - 1)
                    { 
                        attacker.targetPlayer = playerManager;
                        attacker.targetingPlayer = true;
                    }
                }
            }
            if (attacker.isTargeting)
            {
                if (attacker.combatTarget.GetComponent<StatManager>().IsDefeated())
                {
                    attacker.isTargeting = false;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
