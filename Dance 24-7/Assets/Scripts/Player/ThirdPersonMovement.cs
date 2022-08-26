using System.Collections.Generic;
using UnityEngine;
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
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    // Gravity + Jump Variables
    private float gravity = -9.81f;
    public float jumpHeight = 3f;
    public Transform groundCheck;
    private float groundDistance = 0.1f;
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


    // Updates the visual appearance of the health bar based on current health vs total health
    void UpdateHealthBar()
    {
        stats.healthBar.fillAmount = Mathf.Clamp(stats.hp / stats.maxHp, 0, 1f);
    }

    // Uses a collision detector to display a message indicating if an NPC is currently recruitable
    // If recruited the NPC is added to the bandMembers list
    void RecruitBandMember()
    {
        bandMemberDetector = Physics.OverlapSphere(bandMemberCheck.position, checkDistance, bandMember);
        
        if (!combat.activeCombat && bandMemberDetector.Length > 0 && !bandMemberDetector[0].gameObject.GetComponent<NPC>().isFollowing)
        {
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

    // Functions similarly to RecruitBandMember() but is used to revive defeated NPC characters
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

    // Reads mouse input (left/right click) to determine if attacking or defending
    // If attacking, attackFlag is marked true which signals Weapon class activate swinging animation
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


    // When player game object is within certain radius of the ground, jump is made an available action
    void Jump()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    // Enables character control via keyboard + mouse input
    void PlayerMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

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
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            if (!combat.activeCombat)
            {
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            } else
            {
                transform.rotation = Quaternion.Euler(0f, cam.eulerAngles.y, 0f);
            }

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }

    private void Start()
    {
        recruitPrompt.gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
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

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
