using UnityEngine;

public class NPCNavMesh : MonoBehaviour
{

    public GameObject player;
    public ThirdPersonMovement playerManager;
    public CombatManager combat;

    // Helper function for Follow() to instruct first index of bandMembers list to follow player
    void PlayerFollower(NPC firstBandMember)
    {
        firstBandMember.gameObject.GetComponent<NPC>().navAgent.destination = player.transform.position;
    }


    // Manages how NPCs inside bandMembers list follow each other
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

    // Decision making process used to determine enemy target during combat state, targets first available enemy in 
    // agroEnemies that is not currently being targeted by another NPC, if all enemies are targeted, it will target enemy at index 0
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
    }
}
