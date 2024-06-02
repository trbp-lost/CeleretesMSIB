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

    public bool keepChasing;
    public bool enableHide = false;
    private bool isChasing = false;
    private bool isPatrolling = false;
    private bool isFacingRight = true;
    private bool isPlayerHide = false;
    private int audioChasePlayed = 1;

    private Rigidbody2D rb;
    private Transform target;
    private Vector3 originalPosition;
    private Animator animator;
    private PlayerHiding hiding;

    public AudioSource audioSource;
    public AudioClip chaseSound;

    private void Start()
    {
        target = player.GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        if (enableHide) hiding = target.GetComponent<PlayerHiding>();

        originalPosition = transform.position;
        StartCoroutine(Patrolling());
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, target.position);
        if (enableHide) isPlayerHide = hiding.isHiding;
        
        if (distanceToPlayer < attackRange && !isPlayerHide)
        {
            AttackPlayer();
        }
        
        if (distanceToPlayer < chaselRange && !isPlayerHide)
        {
            isChasing = true;

            if (audioSource != null || chaseSound != null)
            {
                if (audioSource.isPlaying == false && audioChasePlayed == 0)
                {
                    audioChasePlayed = 1;
                    audioSource.PlayOneShot(chaseSound);

                }
            }

            ChasePlayer();

        }
        else
        {
            audioChasePlayed = 0;
            if (isChasing && !keepChasing)
            {
                isChasing = false;
                rb.velocity = Vector2.zero; 
            }

            if (keepChasing)
            {
                isChasing = true;
                if (audioSource != null || chaseSound != null)
                {
                    if (audioSource.isPlaying == false && audioChasePlayed == 0)
                    {
                        audioChasePlayed = 1;
                        audioSource.PlayOneShot(chaseSound);

                    }
                }
                ChasePlayer();
            }

            if (!isPatrolling)
            {
                StartCoroutine(Patrolling());
            }

        }

    }

    private void Patrol()
    {
        animator.SetBool("isMove", false);
        int randomX = Random.Range(-patrolRange, patrolRange);
        if (transform.position.x > originalPosition.x + patrolRange || transform.position.x < originalPosition.x - patrolRange)
        {
            randomX = -randomX;
        }

        Vector3 newDirection = new Vector3(randomX, transform.position.y, transform.position.z);
        FaceDirection(0, randomX);

        rb.velocity = new Vector2(newDirection.x * patrolSpeed, rb.velocity.y) ;
    }

    IEnumerator Patrolling()
    {
        isPatrolling = true;
        while (!isChasing)
        {
            Patrol();
            yield return new WaitForSeconds(patrolDelay);
        }

        isPatrolling = false;
    }

    private void ChasePlayer()
    {
        if (player.isDead == true) return;

        animator.SetBool("isMove", true);
        
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
        if (point > target) transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
        if (point < target) transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);

    }

    public void Death()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isPlayerHide)
        {
            Player player = collision.gameObject.GetComponent<Player>();
            
            player.Die();
        }
    }

}
