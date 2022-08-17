using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{

    public Image healthBar;
    public float hp;
    public float maxHp;
    public float defense;
    public float mp;
    public float maxMp;
    public float attackPower;
    public bool isDefending = false;

    public bool IsDefeated()
    {
        if (hp <= 0)
        {
            return true;
        }
        return false;
    }

    public void ReceiveDamage(float attackPower)
    {
        if (isDefending)
        {
            defense = 100;
        } else
        {
            defense = 0;
        }

        float damageDealt = attackPower - defense;
        
        if (damageDealt > 0)
        {
            hp -= damageDealt;
        }
    }

    void UseAbility(float mpCost)
    {
        if (mpCost <= mp)
        {
            mp -= mpCost;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IsDefeated();
    }
}
