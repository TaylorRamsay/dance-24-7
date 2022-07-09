using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    float MoveX = 0;
    float MoveY = 0;
    float MoveZ = 0;

    float Speed = 8;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    { 
        MoveX = Input.GetAxis("Horizontal") * Time.deltaTime * Speed;
        MoveZ = Input.GetAxis("Vertical") * Time.deltaTime * Speed;
        MoveY = Input.GetAxis("Jump") * Time.deltaTime * Speed;
        transform.Translate(MoveX, MoveY, MoveZ);
    }
}
