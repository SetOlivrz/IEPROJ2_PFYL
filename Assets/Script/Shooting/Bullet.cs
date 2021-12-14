using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private float ticks = 0.0f;

    private void FixedUpdate()
    {
        ticks += Time.deltaTime;
        if(ticks >= 5)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colliding");
        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("Collided with " + collision.collider.name);
            Destroy(gameObject);
        }
        
    }
}
