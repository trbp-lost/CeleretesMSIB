using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventResponse : MonoBehaviour
{
    public TriggerEvent triggerObject;
    public Manager manager;

    public AudioSource audioSource;
    public AudioClip audioClip;
    
    public bool destroyRespon;
    public bool IsShowHide;

    private int triggerCounter = 0;

    public void SelfDestroyObject()
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && triggerObject.isTrigger && destroyRespon)
        {
            if (Input.GetKeyDown(KeyCode.F)) triggerCounter += 1;
            Debug.Log(triggerCounter);

            if (triggerCounter == 10)
            {
                SelfDestroyObject();

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && triggerObject.isTrigger && IsShowHide)
        {
            if (audioClip != null) audioSource.PlayOneShot(audioClip);

            manager.MoveToScene("Menu");
        }
    }

}
