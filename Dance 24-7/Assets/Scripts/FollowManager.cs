using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowManager : MonoBehaviour
{

    public GameObject player;
    public ThirdPersonMovement playerMovement;
    public float speedModifier = 100;
    public NPC[] followers;
    public int followersSize;
    public float targetDistance;
    public float maxDistance = 3;
    public RaycastHit search;

    // Helper function to handle NPC immediatel behind player in the konga line
    void PlayerFollower(NPC firstFollower)
    {
        firstFollower.transform.LookAt(player.transform);
        if (Physics.Raycast(firstFollower.transform.position, firstFollower.transform.TransformDirection(Vector3.forward), out search))
        {
            targetDistance = search.distance;

            if (targetDistance >= maxDistance)
            {
                firstFollower.followSpeed = playerMovement.speed / speedModifier;
                firstFollower.transform.position = Vector3.MoveTowards(firstFollower.transform.position, player.transform.position, firstFollower.followSpeed);
            }
            else
            {
                firstFollower.followSpeed = 0;

            }
        }
    }

    // 
    void Follow()
    {

        for (int i = 0; i < followersSize; i++)
        {
            // is writing a separate function to handle the NPC that directly follows the player redundant?
            if (i == 0)
            {

                PlayerFollower(followers[i]);

            } else
            {
                followers[i].transform.LookAt(followers[i - 1].transform);
                if (Physics.Raycast(followers[i].transform.position, followers[i].transform.TransformDirection(Vector3.forward), out search))
                {
                    targetDistance = search.distance;

                    if (targetDistance >= maxDistance)
                    {
                        followers[i].followSpeed = playerMovement.speed / speedModifier;
                        followers[i].transform.position = Vector3.MoveTowards(followers[i].transform.position, followers[i - 1].transform.position, followers[i].followSpeed);
                    }
                    else
                    {
                        followers[i].followSpeed = 0;

                    }
                }
            }
        }
    }

    void Start()
    {

    }

    void Update()
    {
        // Populate followers list

        Follow();
    }
}
