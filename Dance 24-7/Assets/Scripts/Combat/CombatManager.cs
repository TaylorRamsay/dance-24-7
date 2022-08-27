using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public ThirdPersonMovement player;
    public bool activeCombat = false;
    public GameObject combatArena;

    // When the number of enemies in agroEnemies list is greater than 0, game combat state is set to true, all objects listening
    // for state change react accordingly
    bool EnterCombatState()
    {
        if (player.agroEnemies.Count > 0)
        {
            activeCombat = true;
            foreach (NPC x in player.bandMembers)
            {
                x.combatState = true;
            }
            if (!combatArena.activeSelf)
            {
                combatArena.transform.position = new Vector3(player.transform.position.x, .23f, player.transform.position.z);
            }
            combatArena.SetActive(true);
            return true;
        }
        return false;
    }

    // Functions inversely to EnterCombatState(), when agroEnemies list is 0, game combat state is set to false
    bool ExitCombatState()
    {
        if (player.agroEnemies.Count == 0 && activeCombat == true)
        {
            activeCombat = false;
            foreach (NPC x in player.bandMembers)
            {
                x.combatState = false;
                x.navAgent.enabled = true;
                x.isFollowing = true;
                x.isTargeting = false;
                x.combatTarget = null;
            }

            combatArena.SetActive(false);
            return true;
        }

        return false;
    }

    void Start()
    {
        combatArena.SetActive(false);
    }

    void Update()
    {

        EnterCombatState();
        ExitCombatState();
    }
}
