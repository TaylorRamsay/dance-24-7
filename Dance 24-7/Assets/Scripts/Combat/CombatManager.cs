using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{

    public ThirdPersonMovement player;
    public bool activeCombat = false;
    public GameObject combatArena;

    bool EnterCombatState()
    {
        if (player.agroEnemies.Count > 0)
        {
            activeCombat = true;
            foreach (NPC x in player.bandMembers)
            {
                x.combatState = true;
                //x.navAgent.enabled = false;
            }
            if (!combatArena.activeSelf)
            {
                combatArena.transform.position = new Vector3(player.transform.position.x, .23f, player.transform.position.z);
            }
            combatArena.SetActive(true);
            //print(combatArena.transform.position);
            //print(player.transform.position);
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
