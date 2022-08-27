using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject weaponWielder;
    public GameObject rotationAxis;
    public string objectTag;

    public Quaternion origRotation;
    public Quaternion targetRotation;
    public float rotationTime;
    public float speed = 1.5f;
    public bool attackFlag = true;


    // When a game object containing the specified tag collidse with the weapon object it calls the collision objects ReceiveDamage() function which is calculated in the 
    // objects StatManager
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<StatManager>().ReceiveDamage(weaponWielder.GetComponent<StatManager>().attackPower);   
        }
    }

    // When called from within the player class, the player weapon will swing according to the specified rotation (targetRotation) and speed (rotationTime)
    public void PlayerSwingWeapon()
    {
        Transform from = rotationAxis.transform;
        rotationTime = 0;

        if (attackFlag)
        {
            rotationTime += Time.deltaTime * speed;
            rotationAxis.transform.localRotation = Quaternion.Lerp(from.localRotation, targetRotation /*(0, 45, 0)*/, rotationTime);
        }
        if (from.localRotation == targetRotation)
        {
            rotationAxis.transform.localRotation = origRotation;
            attackFlag = false;
        }
    }

    // Functions Identically to PlayerSwingWeapon() with the exception that it is specific to the NPC class
    public void SwingWeapon()
    {
        Transform from = rotationAxis.transform;
        rotationTime = 0;

        if (weaponWielder.GetComponent<NPC>().combatState)
        { 
            rotationTime += Time.deltaTime * speed;
            rotationAxis.transform.localRotation = Quaternion.Lerp(from.localRotation, targetRotation /*(0, 45, 0)*/, rotationTime);
        }
        if (from.localRotation == targetRotation)
        {
            rotationAxis.transform.localRotation = origRotation;
            weaponWielder.GetComponent<NPC>().attackTimer = weaponWielder.GetComponent<NPC>().attackTime;
            attackFlag = false;
        }
    }

    void Start()
    {
        origRotation = rotationAxis.transform.localRotation;
    }
}
