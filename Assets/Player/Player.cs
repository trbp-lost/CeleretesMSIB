using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpForce = 5f;

    private bool isDead = false;
    private Rigidbody2D rbody;
    private Animator animator;

    [HideInInspector] public bool canControl = true;

    private bool _grounded = false;

    public bool Grounded
    {
        private set
        {
            if (_grounded != value)
            {
                _grounded = value;
                animator.SetBool("grounded", _grounded);
            }

        }
        get => _grounded;
    }

    private float _verticalDelta;

    public float VerticalDelta
    {
        private set
        {
            if (_verticalDelta != value)
            {
                _verticalDelta = value;
                animator.SetFloat("yDelta", _verticalDelta);
            }
        }
        get => _verticalDelta;
    }

    private float _hmove;
    public float HMov
    {
        private set
        {

            if (isDead) return;

            if(value != _hmove)
            {
                _hmove = value;

                if (_hmove != 0)
                {
                    FaceRight = _hmove > 0;
                }
            }

            if (!canControl) _hmove = 0;
            
            animator.SetFloat("xSpeed", Mathf.Abs(_hmove));
        }
        get => _hmove;
    }

    private bool _faceRight = true;

    public bool FaceRight
    {
        private set
        {
            if (!canControl) return;

            if (_faceRight != value)
            {
                _faceRight = value;
                Flip();
            }
        }
        get => _faceRight;
    }

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (isDead) return;
        VerticalDelta = rbody.velocity.y;
        HMov = Input.GetAxis("Horizontal");
        Jump();

    }

    private void FixedUpdate()
    {
        Move();
        GroundCheck();

    }

    private void Move()
    {
        rbody.velocity = new Vector2(HMov * moveSpeed, rbody.velocity.y);
    }

    private void Jump()
    {
        if(Grounded && Input.GetKeyDown(KeyCode.Space) && canControl)
        {
            animator.SetTrigger("jumpTrigger");
            rbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        }
    }

    private void GroundCheck()
    {
        RaycastHit2D[] hits = new RaycastHit2D[5];
        int numhits = rbody.Cast(Vector2.down, hits, .5f);
        Grounded = numhits > 0;

    }

    private void Flip()
    {
        Vector3 temp = transform.localScale;
        temp.x *= -1;
        transform.localScale = temp;
    }

    private void Die()
    {
        if (isDead) return;

        isDead = true;
        animator.SetTrigger("deathTrigger");

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Die();
        }
    }
}
