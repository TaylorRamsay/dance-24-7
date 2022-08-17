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

    /*public IEnumerator SwingWeapon(float degreesPerSec)
    {
        float duration = 1;
        float time = 0;
        while (time < duration)
        {
            gameObject.transform.RotateAround(weaponWielder.transform.position, Vector3.up, degreesPerSec * time);
            time += Time.deltaTime;
            print("WEAPON IS SWINGING");
            yield return null;
        }
        while (weaponWielder.GetComponent<NPC>().attackTimer > 0)
        {
            weaponWielder.GetComponent<NPC>().attackTimer -= Time.deltaTime;
        }   
    }
    */

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
        if (attackFlag)
        {
            SwingWeapon();
        }
        
    }
}
