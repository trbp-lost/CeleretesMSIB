using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventResponse : MonoBehaviour
{
    public TriggerEvent triggerObject;

    private int triggerCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelfDestroyObject()
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.collider.tag == "Player" && triggerObject.isTrigger)
        {
            if (Input.GetKeyDown(KeyCode.F)) triggerCounter += 1;
            Debug.Log(triggerCounter);

            if (triggerCounter == 10)
            {
                SelfDestroyObject();

            }
        }
    }
}
