using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCNavMesh : MonoBehaviour
{

    //private NavMeshAgent navMeshAgent;
    public GameObject player;
    public ThirdPersonMovement playerMovement;

    void PlayerFollower(NPC firstBandMember)
    {
        firstBandMember.gameObject.GetComponent<NPC>().navAgent.destination = player.transform.position;
    }

    void Follow()
    {
        for (int i = 0; i < playerMovement.bandMembers.Count; i++)
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

    private void Update()
    {
        Follow();
        //navMeshAgent.destination = player.transform.position;
    }
}
