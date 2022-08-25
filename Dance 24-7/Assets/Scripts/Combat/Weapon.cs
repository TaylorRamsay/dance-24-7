using System.Collections;
using System.Collections.Generic;
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


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<StatManager>().ReceiveDamage(weaponWielder.GetComponent<StatManager>().attackPower);   
        }
    }

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

    void Update()
    {
        /*if (weaponWielder.GetComponent<NPC>().attackFlag)
        {
            SwingWeapon();
        }
        */
    }
}
