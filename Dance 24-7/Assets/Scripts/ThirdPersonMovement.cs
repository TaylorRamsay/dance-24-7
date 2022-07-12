using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam;

    // Player Movement Variables
    public float speed;
    public float walkSpeed = 6f;
    public float runSpeed = 10f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;


    // Gravity + Jump Variables
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    void Update()
    {
        
        // ====== GRAVITY + JUMP HANDLING ====== //
        // Creates a sphere at "groundCheck.position" with radius "groundDistance" to check for collision with objects specified as "groundMask"
        // Returns True if collision exists, False if collision does not exist
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Applys a constant low gravity velocity of -2f if grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Increments gravity value to player while not grounded, Time.deltaTime to keep frame rate independent
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);


        // ====== MOVEMENT HANDLING ====== //
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        
        
        // Is there a more efficient way to do this??
        if(Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        } else
        {
            speed = walkSpeed;
        }

        if(direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
}
