using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MansionTo2 : MonoBehaviour
{
    private bool isPlayerInRange = false;


    Faded fade;

    void Start()
    {
        fade = FindObjectOfType<Faded>();
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // Ganti "Mansion-Sub1" dengan nama scene yang ingin Anda tuju
            SceneManager.LoadScene("Mansion-Sub2");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Player in range. Press 'E' to open the door.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("Player out of range.");
        }
    }
}
