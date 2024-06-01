using UnityEngine;

public class HideSpot : MonoBehaviour
{
    private bool isPlayerInRange = false;

    private void Start()
    {
        // Ensure the object has the correct tag and collider setup
        if (!CompareTag("HideSpot"))
        {
            Debug.LogWarning("HideSpot object does not have the correct tag assigned.");
        }

        Collider2D collider = GetComponent<Collider2D>();
        if (collider == null || !collider.isTrigger)
        {
            Debug.LogError("HideSpot object needs a Collider2D component with 'Is Trigger' enabled.");
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Handle the interaction here
            Debug.Log("Player interacted with HideSpot.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Player entered HideSpot range.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("Player exited HideSpot range.");
        }
    }
}
