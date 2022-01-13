using UnityEngine;
using UnityEngine.UI;

public class CharacterMotor : MonoBehaviour
{
    //Animation du personnage
    Animator animator;
    Rigidbody playerRb;
    PlayerInventory playerInventory;
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
    public GameObject rayHit;
    public float attackRange;

    //Variables sort
    [Header("Paramètre sort")]
    public int sortCourant = 1;
    public int sortTotal;
    private GameObject rayHitSort;
    private GameObject spellHolderImage;

    //Sort Foudre
    [Header("Paramètre sort Foudre")]
    public GameObject sortFoudreGameObject;
    public float sortFroudreCoutMana;
    public float sortFroudreVitesse;
    public int sortFoudreID;
    public Sprite sortFoudreImage;

    //Sort Soin
    [Header("Paramètre sort Soin")]
    public GameObject sortSoinGameObject;
    public float sortSoinCoutMana;
    public float sortSoinMontant;
    public int sortSoinID;
    public Sprite sortSoinImage;


    public Vector3 jumpSpeed;
    CapsuleCollider playerCollider;

    public bool isOnGround;
    public bool isInShop;
    public bool isDead = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<CapsuleCollider>();
        playerInventory = GetComponent<PlayerInventory>();
        playerRb = GetComponent<Rigidbody>();
        rayHit = GameObject.Find("RayHit");
        rayHitSort = GameObject.Find("RayHitSpell");
        spellHolderImage = GameObject.Find("SpellHolderImage");
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
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                LancerSort();
            }
            //Systeme cooldown
            if (isAttacking)
            {
                currentCooldown -= Time.deltaTime;
            }
            if (currentCooldown <= 0)
            {
                currentCooldown = attackCooldown;
                isAttacking = false;
            }
            //Changement sort avec molette down
            if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                if (sortCourant <= sortTotal && sortCourant != 1)
                {
                    sortCourant--;
                }
            }
            //Changement sort avec molette down
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                if (sortCourant >= 0 && sortCourant != sortTotal)
                {
                    sortCourant++;
                }
            }
            //Changement image suivant sort selectionner
            if (sortCourant == sortFoudreID)
            {
                spellHolderImage.GetComponent<Image>().sprite = sortFoudreImage;
            }
            if (sortCourant == sortSoinID)
            {
                spellHolderImage.GetComponent<Image>().sprite = sortSoinImage;
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

            if (Physics.Raycast(rayHit.transform.position, transform.TransformDirection(Vector3.forward), out hit, attackRange))
            {
                Debug.DrawLine(rayHit.transform.position, hit.point, Color.red);

                if (hit.transform.tag == "Enemy")
                {
                    hit.transform.GetComponent<EnemyAI>().AppliquerDegat(playerInventory.currentDamage);
                    Debug.Log(hit.transform.name + "detected");
                }
            }

            isAttacking = true;
        }


        //animator.SetBool("Attack_1",true);
    }
    public void LancerSort()
    {
        //Sort de Foudre
        if (sortCourant == sortFoudreID && !isAttacking && playerInventory.currentMana >= sortFroudreCoutMana)
        {
            animator.SetTrigger("Attack");
            GameObject sort = Instantiate(sortFoudreGameObject, rayHitSort.transform.position, transform.rotation);
            sort.GetComponent<Rigidbody>().AddForce(transform.forward * sortFroudreVitesse);
            playerInventory.currentMana -= sortFroudreCoutMana;
            isAttacking = true;
        }
        //Sort de Soin
        if (sortCourant == sortSoinID && !isAttacking && playerInventory.currentMana >= sortFroudreCoutMana && playerInventory.currentHealth < playerInventory.maxHealth)
        {
            animator.SetTrigger("Attack");
            Instantiate(sortSoinGameObject, rayHitSort.transform.position, transform.rotation);
            playerInventory.currentMana -= sortSoinCoutMana;
            playerInventory.currentHealth += sortSoinMontant;
            isAttacking = true;
        }


        //animator.SetBool("Attack_1",true);
    }
}
