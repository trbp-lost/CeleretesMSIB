using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player; 
    public float speed = 5f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Folllow();
    }

    private void Folllow()
    {
        if (player != null) 
        {
            Vector3 direction = player.position - transform.position;

            direction.Normalize();

            transform.position += direction * speed * Time.deltaTime;
        }
    }
}
