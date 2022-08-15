using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject weaponWielder;
    public string objectTag;
    public Vector3 endSwingPosition;
    public float duration;


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<StatManager>().ReceiveDamage(weaponWielder.GetComponent<StatManager>().attackPower);   
        }
    }

    public IEnumerator SwingWeapon(Vector3 targetPosition, float duration = 5)
    {
        float time = 0;
        Vector3 startPosition = transform.position;
        while (time < duration)
        {
            gameObject.transform.position = Vector3.Lerp(startPosition, startPosition + targetPosition, time / duration);
            time += Time.deltaTime;

            print("Start position: " + startPosition);
            print("Current position: " + gameObject.transform.position);
            print("Target position: " + targetPosition);


            yield return null;
        }
        gameObject.transform.position = startPosition + targetPosition;
        //gameObject.transform.position = weaponWielder.transform.position;
    }

    void Start()
    {
        //StartCoroutine(SwingWeapon(endSwingPosition, duration));
    }

    void Update()
    {
        //SwingWeapon(endSwingPosition, 5f);
    }
}
