using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowKeyInput : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode keyCode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(keyCode))
                {
                    Debug.Log("Key pressed: " + keyCode);
                    break;
                }
            }
        }
    }
}
