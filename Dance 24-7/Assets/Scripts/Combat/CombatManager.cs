using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{

    public ThirdPersonMovement player;
    public bool activeCombat = false;

    bool EnterCombatState()
    {
        if (player.agroEnemies.Count > 0)
        {
            activeCombat = true;
            foreach (NPC x in player.bandMembers)
            {
                x.combatState = true;
            }
                
            return true;
        }
        return false;
    }

    bool ExitCombatState()
    {
        if (player.agroEnemies.Count == 0 && activeCombat == true)
        {
            activeCombat = false;
            foreach (NPC x in player.bandMembers)
            {
                x.combatState = false;
                x.isFollowing = true;

            }

            return true;
        }

        return false;
    }


    void Start()
    {

    }


    void Update()
    {

        EnterCombatState();
        ExitCombatState();
    }
}
