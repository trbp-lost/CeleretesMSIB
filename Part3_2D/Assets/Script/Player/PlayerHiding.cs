using UnityEngine;

public class PlayerHiding : MonoBehaviour
{
    private Rigidbody2D rb;
    private float dirX;
    private float moveSpeed = 10f;
    private SpriteRenderer rend;
    private bool canHide = false;
    private bool hiding = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal") * moveSpeed;

        if (canHide && Input.GetKeyDown(KeyCode.E))
        {
            hiding = !hiding;

            if (hiding)
            {
                EnterHideSpot();
            }
            else
            {
                ExitHideSpot();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!hiding)
        {
            rb.velocity = new Vector2(dirX, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void EnterHideSpot()
    {
        Physics2D.IgnoreLayerCollision(8, 9, true);
        rend.sortingOrder = 0;
        Debug.Log("Player is now hiding.");
    }

    private void ExitHideSpot()
    {
        Physics2D.IgnoreLayerCollision(8, 9, false);
        rend.sortingOrder = 2;
        Debug.Log("Player is no longer hiding.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("HideSpot"))
        {
            canHide = true;
            Debug.Log("Player can hide.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("HideSpot"))
        {
            canHide = false;
            Debug.Log("Player can no longer hide.");
        }
    }
}
