using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public ThirdPersonMovement playerMovement;
    public GameObject npc;
    public float targetDistance;
    public float maxDistance = 3;
    public float followSpeed;
    public RaycastHit search;


    void Update()
    {
        transform.LookAt(player.transform);

        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out search))
        {
            targetDistance = search.distance;
            
            if (targetDistance >= maxDistance)
            {
                followSpeed = playerMovement.speed / 100;
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, followSpeed); 
            }
            else
            {
                followSpeed = 0;

            }
        }
    }
}
