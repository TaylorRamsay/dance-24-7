using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public GameObject weaponWielder;
    public GameObject rotationAxis;
    public string objectTag;

    public Quaternion origRotation;
    public Quaternion targetRotation;
    public float rotationTime;
    float speed = 5f;
    public bool attackFlag = true;


    // When object with specified tag makes contact with weapon object, ReceiveDamage() function is called on collision object, damage statistics calculated in StatManager
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Band Member") || collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<StatManager>().ReceiveDamage(weaponWielder.GetComponent<StatManager>().attackPower);
        }
    }

    // When called, weapon object is rotated based on targetRotation and speed (rotationTime)
    public void SwingWeapon()
    {
        Transform from = rotationAxis.transform;
        rotationTime = 0;

        if (weaponWielder.GetComponent<EnemyNPC>().combatState)
        {
            rotationTime += Time.deltaTime * speed;
            rotationAxis.transform.localRotation = Quaternion.Lerp(from.localRotation, targetRotation /*(0, 45, 0)*/, rotationTime);
        }
        if (from.localRotation == targetRotation)
        {
            rotationAxis.transform.localRotation = origRotation;
            weaponWielder.GetComponent<EnemyNPC>().attackTimer = weaponWielder.GetComponent<EnemyNPC>().attackTime;
            attackFlag = false;
        }
    }

    void Start()
    {
        origRotation = rotationAxis.transform.localRotation;
    }

    void Update()
    {
        if (weaponWielder.GetComponent<EnemyNPC>().attackFlag)
        {
            SwingWeapon();
        }

    }
}
