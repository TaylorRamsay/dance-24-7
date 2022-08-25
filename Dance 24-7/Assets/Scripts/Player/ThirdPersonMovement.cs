using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ThirdPersonMovement : MonoBehaviour
{

    public CharacterController controller;
    public Transform cam;
    public StatManager stats;

    // Player Movement Variables
    public float speed;
    public float walkSpeed = 6f;
    public float runSpeed = 10f;

    // Represents the time it takes for the player to turn
    public float turnSmoothTime = 0.1f;
    // Used to hold the velocity at which the player will turn from current facing angle to target angle
    float turnSmoothVelocity;

    // Gravity + Jump Variables
    private float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    private float groundDistance = 0.4f;
    public LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;

    // Recruit Variables
    public List<NPC> bandMembers;
    public Transform bandMemberCheck;
    public float checkDistance = 1f;
    public LayerMask bandMember;
    public static Collider[] bandMemberDetector;
    [SerializeField] private TextMeshProUGUI recruitPrompt;

    // Revive Variables
    public Transform reviveCheck;
    public LayerMask instrument;
    public static Collider[] reviveDetector;
    [SerializeField] private TextMeshProUGUI revivePrompt;

    // Combat Variables
    public CombatManager combat;
    public List<EnemyNPC> agroEnemies;
    public Weapon playerWeapon;


    void UpdateHealthBar()
    {
        stats.healthBar.fillAmount = Mathf.Clamp(stats.hp / stats.maxHp, 0, 1f);
    }

    void RecruitBandMember()
    {
        bandMemberDetector = Physics.OverlapSphere(bandMemberCheck.position, checkDistance, bandMember);
        
        if (!combat.activeCombat && bandMemberDetector.Length > 0 && !bandMemberDetector[0].gameObject.GetComponent<NPC>().isFollowing)
        {
            // display "Press E to recruit band member" button prompt
            recruitPrompt.gameObject.SetActive(true);
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                bandMembers.Add(bandMemberDetector[0].gameObject.GetComponent<NPC>());
                bandMemberDetector[0].gameObject.GetComponent<NPC>().isFollowing = true;
            }
        }
        else
        {
            recruitPrompt.gameObject.SetActive(false);
        }
    }

    void ReviveBandMember()
    {
        if (!combat.activeCombat)
        {
            reviveDetector = Physics.OverlapSphere(reviveCheck.position, checkDistance, instrument);

            if (reviveDetector.Length > 0 && reviveDetector[0].GetComponent<Weapon>().weaponWielder.GetComponent<NPC>().stats.hp <= 0)
            {
                revivePrompt.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    reviveDetector[0].GetComponent<Weapon>().weaponWielder.GetComponent<NPC>().EnableOnRessurection();
                }
            } else
            {
                revivePrompt.gameObject.SetActive(false);
            }
        }
    }

    void Combat()
    {
        if (Input.GetMouseButton(1))
        {
            stats.isDefending = true;
        } else
        {
            stats.isDefending = false;
        }

        if (Input.GetMouseButtonDown(0) && stats.isDefending == false)
        {
            playerWeapon.attackFlag = true;
        }
    }


    void Jump()
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

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void PlayerMovement()
    {
        // ====== MOVEMENT HANDLING ====== //
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        // Is there a more efficient way to do this??
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }
        else
        {
            speed = walkSpeed;
        }

        if (direction.magnitude >= 0.1f)
        {
            // "targetAngle" stores the angle we want the player to face, Atan2 returns an angle starting at 0 and terminating at x,z(in this instance)
            // Rad2Deg is used to convert from radians to usable degrees
            // " + cam.eulerAngles.y" adds the current camera y-axis rotation to the "targetAngle" so player angle will be influenced by camera rotation
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            // enables the player to smoothly turn between current angle and "targetAngle", rather than snapping
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            // Handles direction player faces inside/outside of combat
            if (!combat.activeCombat)
            {
                // Rotates the player on the y-axis to face the current direction of movement.....Format: "Euler(x, y, z)"
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            } else
            {
                // Rotaties the player on the y-axis to face the current direction of the camera
                transform.rotation = Quaternion.Euler(0f, cam.eulerAngles.y, 0f);
            }

            // Multiplying by " * Vector3.forward" turns the rotation into a direction
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }

    private void Start()
    {
        recruitPrompt.gameObject.SetActive(false);
    }

    void Update()
    {
        UpdateHealthBar();
        RecruitBandMember();
        ReviveBandMember();
        Combat();
        PlayerMovement();
        Jump();
        if (playerWeapon.attackFlag)
        {
            playerWeapon.PlayerSwingWeapon();
        }
        if (stats.hp <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // Increments gravity value to player while not grounded, Time.deltaTime to keep frame rate independent
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
