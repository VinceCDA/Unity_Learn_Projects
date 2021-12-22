using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
    //Animation du personnage
    Animator animator;
    Rigidbody playerRb;
    //Vitesse de déplacement
    public float walkSpeed;
    public float runSpeed;
    public float turnSpeed;
    public float jumpForce;

    //Input
    public string inputFront;
    public string inputBack;
    public string inputLeft;
    public string inputRight;

    //Variables d'attaque
    public float attackCooldown;
    private bool isAttacking;
    private float currentCooldown;
    public GameObject rayhit;
    public float attackRange;

    public Vector3 jumpSpeed;
    CapsuleCollider playerCollider;

    public bool isOnGround;

    public bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider>();
        playerRb = GetComponent<Rigidbody>();
        rayhit = GameObject.Find("RayHit");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            //Idle
            if (!Input.GetKeyDown(inputFront) && !Input.GetKey(inputBack))
            {
                animator.SetFloat("Speed_f", 0.0f);
                animator.SetFloat("Speed_Multiplier", 1.0f);
            }
            //Avancer
            if (Input.GetKey(inputFront) && !Input.GetKey(KeyCode.LeftShift))
            {

                transform.Translate(0, 0, walkSpeed * Time.deltaTime);
                animator.SetFloat("Speed_f", 0.5f);
                animator.SetFloat("Speed_Multiplier", 1.0f);
            }
            //Sprint
            if (Input.GetKey(inputFront) && Input.GetKey(KeyCode.LeftShift))
            {
                transform.Translate(0, 0, runSpeed * Time.deltaTime);
                animator.SetFloat("Speed_f", 0.5f);
                animator.SetFloat("Speed_Multiplier", 2.0f);
            }
            //Reculer
            if (Input.GetKey(inputBack))
            {
                transform.Translate(0, 0, -(walkSpeed / 2) * Time.deltaTime);
                animator.SetFloat("Speed_f", 0.5f);
                animator.SetFloat("Speed_Multiplier", 0.5f);

            }
            //Rotation gauche
            if (Input.GetKey(inputLeft))
            {
                transform.Rotate(0, -turnSpeed, 0);
            }
            //Rotation droite
            if (Input.GetKey(inputRight))
            {
                transform.Rotate(0, turnSpeed, 0);
            }
            //Saut
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isOnGround = false;
            }
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
            }
            if (isAttacking)
            {
                currentCooldown -= Time.deltaTime;
            }
            if (currentCooldown <= 0)
            {
                currentCooldown = attackCooldown;
                isAttacking = false;
            }
        }
        
    }
    bool isGrounded()
    {
        return Physics.CheckCapsule(playerCollider.bounds.center, new Vector3(playerCollider.bounds.center.x, playerCollider.bounds.min.y - 0.1f, playerCollider.bounds.center.z), 0.21f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            
        }

    }
    public void Attack()
    {
        if (!isAttacking)
        {
            animator.SetTrigger("Attack");

            RaycastHit hit;

            if (Physics.Raycast(rayhit.transform.position,transform.TransformDirection(Vector3.forward),out hit,attackRange))
            {
                Debug.DrawLine(rayhit.transform.position, hit.point, Color.red);

                if (hit.transform.tag == "test")
                {
                    Debug.Log(hit.transform.name + "detected");
                }
            }

            isAttacking = true;
        }
        
        
        //animator.SetBool("Attack_1",true);
    }
}
