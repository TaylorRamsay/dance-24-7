using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNavMesh : MonoBehaviour
{

    //private NavMeshAgent navMeshAgent;
    public GameObject player;
    public ThirdPersonMovement playerManager;
    public CombatManager combat;




    public void NPCTargeting(EnemyNPC attacker)
    {
        if (combat.activeCombat)
        {
            if (attacker.isTargeting == false)
            {
                for (int i = 0; i < playerManager.bandMembers.Count; i++)
                {
                    if (playerManager.bandMembers[i].GetComponent<NPC>().targeted == false)
                    {
                        attacker.combatTarget = playerManager.bandMembers[i];
                        attacker.isTargeting = true;
                        playerManager.bandMembers[i].GetComponent<NPC>().targeted = true;

                        print(attacker + " is targeting " + attacker.combatTarget);

                        goto after;
                    }
                    else if (i == playerManager.bandMembers.Count - 1)
                    {
                        attacker.combatTarget = playerManager.bandMembers[0];
                        attacker.isTargeting = true;
                        goto after;
                    }
                }
            }
            else
            {
                attacker.gameObject.transform.LookAt(attacker.combatTarget.transform);
                if (attacker.combatTarget.GetComponent<StatManager>().IsDefeated())
                {
                    attacker.isTargeting = false;
                }
            }
        after:;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
