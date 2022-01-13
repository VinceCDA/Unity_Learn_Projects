using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //Distance entre joueur et ennemi
    private float distance;
    //Distance entre base et ennemi
    private float distanceBase;
    //Position de base de l'ennemi
    private Vector3 basePosition;
    //Cible de l'ennemi
    public Transform Target;
    //Distance de poursuite
    public float chaseRange = 10.0f;
    //Portée de l'attaque
    public float attackRange = 2.2f;
    //Cooldown des attaques
    public float attackRepeatTime = 1.0f;
    private float attackTime;
    //Montant des dégats
    public float attackDamage;
    //animation de l'ennemi
    private Animator animator;
    //Loot de l'enemi
    public GameObject[] loots;
    //Vie de l'ennemi
    public float ennemiPointDeVie;
    //Mort
    public bool estMort;

    //Agent de navigation
    private UnityEngine.AI.NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();
        attackTime = Time.time;
        basePosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!estMort)
        {
            //Recherche du joueur en permanence
            Target = GameObject.Find("Player").transform;
            //Calcul de la distance entre joueur et ennemi
            distance = Vector3.Distance(Target.position, transform.position);
            //Calcul de la distance entre joueur et ennemi
            distanceBase = Vector3.Distance(basePosition, transform.position);
            //Quand l'ennemi est loin
            if (distance > chaseRange && distanceBase <= 1)
            {
                Idle();
            }
            //Quand l'ennemi est proche mais pas assez pour attaquer
            if (distance < chaseRange && distance > attackRange)
            {
                Chase();
            }
            //Quand l'ennemi est assez proche pour attaquer
            if (distance < attackRange)
            {
                Attack();
            }
            //Quand joueur s'echappe
            if (distance > chaseRange && distanceBase > 1)
            {
                RetourBase();
            }
        }
    }
    //poursuite
    void Chase()
    {
        animator.SetBool("isMoving", true);
        agent.destination = Target.position;
    }
    //Combat
    void Attack()
    {
        Idle();
        animator.SetTrigger("Attack");
        agent.destination = transform.position;
        //Si pas de cooldown
        if (Time.time > attackTime)
        {
            Target.GetComponent<PlayerInventory>().ApplyDamage(attackDamage);
            Debug.Log("L'ennemi a infligé" + attackDamage + "points de dégats");
            attackTime = Time.time + attackRepeatTime;
        }
    }
    //Idle
    void Idle()
    {
        animator.SetBool("isMoving", false);
    }
    //Degats recus
    public void AppliquerDegat(float degatEntrant)
    {
        ennemiPointDeVie = ennemiPointDeVie - degatEntrant;
        Debug.Log(gameObject.name + "a subit" + degatEntrant + "points de dégats");
        if (ennemiPointDeVie <= 0)
        {
            Mort();
        }
    }
    public void RetourBase()
    {
        animator.SetBool("isMoving", true);
        agent.destination = basePosition;
    }
    //Mort
    public void Mort()
    {
        estMort = true;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        animator.SetBool("isMoving", false);
        animator.SetBool("isDead", true);
        GameObject drop = loots[Random.Range(0, loots.Length)];
        Instantiate(drop, transform.position, transform.rotation);
    }
}
