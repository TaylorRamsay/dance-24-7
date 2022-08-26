using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    public Image healthBar;
    public float hp;
    public float maxHp;
    public float defense;
    public float attackPower;
    public bool isDefending = false;

    // When hp reaches 0, returns true which marks the cooresponding character as defeated, disabling specific components
    public bool IsDefeated()
    {
        if (hp <= 0)
        {
            return true;
        }
        return false;
    }

    // Used to calculate the amount of damage taken when attacked, currently receives full damage if not blocking, or 0 damage if blocking
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

    void Update()
    {
        IsDefeated();
    }
}
