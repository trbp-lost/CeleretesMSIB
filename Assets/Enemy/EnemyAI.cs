using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;

    public float attackRange = 1f;
    public int patrolRange = 5;
    public float chaselRange = 5f;
    public float patrolDelay = 3f;
    public Player player;

    private bool isChasing = false;
    private bool isPatrolling = false;
    private bool isfacingRight = false;

    private Rigidbody2D rb;
    private Transform target;
    private Vector3 originalPosition;
    private Animator animator;

    private void Start()
    {
        target = player.GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        originalPosition = transform.position;
        animator = GetComponent<Animator>();
        StartCoroutine(Patrolling());
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);

        if (distanceToPlayer < attackRange)
        {
            AttackPlayer();
        }
        
        if (distanceToPlayer < chaselRange)
        {
            isChasing = true;
            animator.SetBool("isMove", false);
            ChasePlayer();

        }
        else
        {
            if (isChasing)
            {
                isChasing = false;
                rb.velocity = Vector2.zero; 
            }

            if (!isPatrolling)
            {
                StartCoroutine(Patrolling());
            }

            if (!isPatrolling && !isChasing)
            {
                animator.SetBool("isMove", false);
            }

        }

    }

    private void Patrol()
    {

        int randomX = Random.Range(-patrolRange, patrolRange);
        if (transform.position.x > originalPosition.x + patrolRange || transform.position.x < originalPosition.x - patrolRange)
        {
            randomX = -randomX;
        }
        Debug.Log("rng =" + randomX + "\ntransform = " + transform.position.x);

        Vector3 newDirection = new Vector3(randomX, transform.position.y, transform.position.z);
        FaceDirection(0, randomX);

        rb.velocity = new Vector2(newDirection.x * patrolSpeed, rb.velocity.y) ;
    }

    IEnumerator Patrolling()
    {
        isPatrolling = true;
        animator.SetBool("isMove", isPatrolling);
        Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");

        while (!isChasing)
        {
            Patrol();
            yield return new WaitForSeconds(patrolDelay);
        }

        isPatrolling = false;
        animator.SetBool("isMove", isPatrolling);
        Debug.Log("bbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbb");


    }

    private void ChasePlayer()
    {
        animator.SetBool("isMove", isChasing);

        Vector2 direction = (target.position - transform.position).normalized;
        Vector3 chaseLenght = transform.position + new Vector3(direction.x, direction.y, transform.position.z);
        FaceDirection(transform.position.x, chaseLenght.x);

        rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y);
    }

    private void AttackPlayer()
    {
        Debug.Log("Attacking the player!");
    }

    private void FaceDirection(float point, float target)
    {
        if (point > target ) transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        if (point < target ) transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

    }

    public void Death()
    {
        Destroy(gameObject);
    }


}
