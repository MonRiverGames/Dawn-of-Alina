using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public class EnemyController : MonoBehaviour
{
    public EnemyData enemyData;
    public Animator anim;
    public NavMeshAgent navMeshAgent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;
    public float maxHealth;
    public float currentHealth;
    public bool isDead;

    //Patrolling
    public Vector3 walkPoint;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange, walkPointSet;

    public GameObject drop;
    private Transform enPos; //Enemy position


    // Start is called before the first frame update
    void Start()
    {
        sightRange = enemyData.detectRange;
        attackRange = enemyData.attackRange;
        timeBetweenAttacks = enemyData.timeBetweenAttacks;
        maxHealth = enemyData.health;
        currentHealth = maxHealth;
        walkPointRange = enemyData.walkPointRange;

        player = GameObject.Find("Alina").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            //Check for sight and attack range
            playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

            if (!playerInSightRange && !playerInAttackRange)
            {
                Patroling();
            }

            if (playerInSightRange && !playerInAttackRange)
            {
                ChasePlayer();
            }

            if (playerInAttackRange && playerInSightRange)
            {
                AttackPlayer();
            }
        }

    }


    private void Patroling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }

        if (walkPointSet && !isDead)
        {
            navMeshAgent.SetDestination(walkPoint); 

            //Add walk animation code
            anim.SetBool("isWalking", true);
            anim.SetBool("isRunning", false);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //WalkPoint Reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            StartCoroutine(WaitToFindNewPoint());
        }
    }

    public IEnumerator WaitToFindNewPoint()
    {
        anim.SetBool("isWalking", false);
        anim.SetBool("isWalking", false);
        yield return new WaitForSeconds(4);
        walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 1f, whatIsGround))
        {
            walkPointSet = true;
        }
    }



    private void ChasePlayer()
    {
        navMeshAgent.SetDestination(player.position);
        transform.LookAt(player);
        //Add run animation code
        anim.SetBool("isRunning", true);
        anim.SetBool("isWalking", false);
    }

    private void AttackPlayer()
    {
        //Makes sure enemy doesn't move
        navMeshAgent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            //Add Attack animation
            anim.SetTrigger("Attack");
            //Add attack damage

            alreadyAttacked = true;

            StartCoroutine(ResetAttack());
        }


    }



    public IEnumerator ResetAttack()
    {

        yield return new WaitForSeconds(timeBetweenAttacks);
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Hit");
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            isDead = true;
            navMeshAgent.enabled = false;
            anim.SetTrigger("Death");

            // Instantiate(drop, enPos.position, Quaternion.identity);
            // Instantiate(drop, transform.position, Quaternion.identity);     // Drop an item upon destruction of enemy
            Destroy(gameObject, 4f);

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    public Boolean IsItDead()
    {
        return isDead;
    }


}