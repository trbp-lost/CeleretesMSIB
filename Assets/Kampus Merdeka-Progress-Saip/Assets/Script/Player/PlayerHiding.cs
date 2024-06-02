using UnityEngine;

public class PlayerHiding : MonoBehaviour
{
    public bool isHiding = false;
    private bool isNearHideSpot = false;
    private Transform hideSpot;
    private Vector3 originalPosition;
    //private Movement playerMovement;
    private Player playerMovement;
    private Collider2D playerCollider;
    private Rigidbody2D rb;

    private void Start()
    {

        // Get the PlayerMovement component attached to the player
        //playerMovement = GetComponent<Movement>();
        playerMovement = GetComponent<Player>();
        if (playerMovement == null)
        {
            Debug.LogError("Movement component not found on the player.");
        }

        // Get the Rigidbody2D component attached to the player
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component not found on the player.");
        }
    }

    void Update()
    {
        if (isNearHideSpot && Input.GetKeyDown(KeyCode.E))
        {
            if (isHiding)
            {
                ExitHideSpot();
            }
            else
            {
                EnterHideSpot();
            }
        }

    }

    private void EnterHideSpot()
    {
        if (hideSpot == null)
        {
            Debug.LogWarning("hideSpot is null. Cannot enter hide spot.");
            return;
        }

        isHiding = true;

        // Check if SpriteRenderer and Collider2D are attached to the player
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Collider2D collider = GetComponent<Collider2D>();

        if (spriteRenderer == null || collider == null)
        {
            Debug.LogError("Missing components: SpriteRenderer or Collider2D is not attached to the player.");
            return;
        }

        // Save the original position before hiding
        originalPosition = transform.position;

        // Disable the Movement script first and stop player movement
        if (playerMovement != null)
        {
            playerMovement.enabled = false;
        }

        // Stop player movement by setting velocity to zero
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
        }

        // Disable player renderer and collider
        spriteRenderer.enabled = false;
        collider.enabled = true;

        // Move player to hide spot position
        transform.position = hideSpot.position;

        Debug.Log("Entered hide spot: " + hideSpot.name);
    }

    private void ExitHideSpot()
    {
        // No need to check if hideSpot is null here because we already ensure it's not null before calling this method
        isHiding = false;

        // Check if SpriteRenderer and Collider2D are attached to the player
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Collider2D collider = GetComponent<Collider2D>();

        if (spriteRenderer == null || collider == null)
        {
            Debug.LogError("Missing components: SpriteRenderer or Collider2D is not attached to the player.");
            return;
        }

        // Enable player renderer and collider
        spriteRenderer.enabled = true;
        collider.enabled = true;

        // Move player back to original position
        transform.position = originalPosition;

        // Enable the Movement script
        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }

        Debug.Log("Exited hide spot.");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HideSpot"))
        {
            isNearHideSpot = true;
            hideSpot = collision.transform;
            Debug.Log("Player is near hide spot: " + hideSpot.name);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("HideSpot"))
        {
            isNearHideSpot = false;
            hideSpot = null;
            Debug.Log("Player left the hide spot area.");
        }
    }
}
