using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEventResponse : MonoBehaviour
{
    public TriggerEvent triggerObject;
    public bool destroyRespon;
    public bool showHideGameobject;

    private int triggerCounter = 0;

    void Update()
    {
        
    }

    public void SelfDestroyObject()
    {
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {

        if (collision.collider.tag == "Player" && triggerObject.isTrigger && destroyRespon == true)
        {
            if (Input.GetKeyDown(KeyCode.F)) triggerCounter += 1;
            Debug.Log(triggerCounter);

            if (triggerCounter == 10)
            {
                SelfDestroyObject();

            }
        }

        if (collision.collider.tag == "Player" && triggerObject.isTrigger && showHideGameobject == true)
        {
            gameObject.SetActive(!gameObject.active);
        }
    }

}
