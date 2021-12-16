using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private float ticks = 0.0f;
    private Vector3 firepoint;
    private float speed = 20f;
    private void FixedUpdate()
    {
        ticks += Time.deltaTime;
        if (ticks >= 5)
        {
            Destroy(gameObject);
        }
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colliding");
        if (collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("Collided with " + collision.collider.name);
            EnemyBehavior enemy = collision.collider.GetComponent<EnemyBehavior>();
            enemy.ReceiveDamage(10);
            Destroy(gameObject);
        }

    }
}
