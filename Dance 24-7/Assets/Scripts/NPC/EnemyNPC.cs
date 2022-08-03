using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNPC : MonoBehaviour
{
    public GameObject player;
    public ThirdPersonMovement playerMovement;
    public NavMeshAgent navAgent;
    public GameObject npc;

    public bool agro = false;
    public GameObject agroIdentifier;

    public float hp;
    public float mp;
    public float attackPower;
    public float movementSpeed;


    void HealthCheck()
    {
        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    void Start()
    {
        agroIdentifier.SetActive(false);
    }


    void Update()
    {
        HealthCheck();
        if (agro)
        {
            agroIdentifier.SetActive(true);
        }
    }
}
