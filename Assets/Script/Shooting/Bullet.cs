using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private float ticks = 0.0f;
    private Vector3 firepoint;
    private float speed = 30f;
    private void FixedUpdate()
    {
        ticks += Time.deltaTime;
        if (ticks >= 5)
        {
            Destroy(gameObject);
        }
        transform.position += this.transform.forward * Time.deltaTime * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colliding");
        if (collision.collider.CompareTag("Enemy"))
        {
           // Debug.Log("Collided with " + collision.collider.name);
            EnemyBehavior enemy = collision.collider.GetComponent<EnemyBehavior>();
            if(enemy.enemyName == "Normal Slime") enemy.ReceiveDamage(30);
            else enemy.ReceiveDamage(10);
            Destroy(gameObject);
        }

    }
}
