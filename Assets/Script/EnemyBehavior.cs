using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    //Target
    protected Transform player;

    //Enemy Stats
    [SerializeField] private float speed = 2f;
    public float health = 50.0f;
    public float damage = 20.0f;
    
    //Movement
    private float prev_x;
    private float prev_z;

    //Sprite
    [SerializeField] private Sprite Right;
    [SerializeField] private Sprite Left;
    [SerializeField] private Sprite Up;
    [SerializeField] private Sprite Down;

    //Interval
    private float ticks = 0.0f;
    private float ATTACK_INTERVAL = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
       player = GameObject.FindGameObjectWithTag("Player").transform;
       prev_x = transform.position.x;
       prev_z = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        UpdateSprite();
    }

    void UpdateSprite()
    {
        if (prev_x < transform.position.x)
        {
            this.GetComponent<SpriteRenderer>().sprite = Right;
            //Debug.Log("Moving Right");
        }

        else if (prev_x > transform.position.x)
        {
            this.GetComponent<SpriteRenderer>().sprite = Left;
            //Debug.Log("Moving Left");
        }

        else if (prev_z < transform.position.z)
        {
            this.GetComponent<SpriteRenderer>().sprite = Up;
            //Debug.Log("Moving Up");
        }

        else if (prev_z > transform.position.z)
        {
            this.GetComponent<SpriteRenderer>().sprite = Down;
            //Debug.Log("Moving Down");
        }

        prev_x = transform.position.x;
        prev_z = transform.position.z;
    }

    public void ReceiveDamage(float damage)
    {
        this.health -= damage;
    }

    private void OnTriggerStay(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            this.ticks += Time.deltaTime;
            if (ticks > ATTACK_INTERVAL)
            {
                ticks = 0.0f;
                //player.health -= damage;
                Debug.Log("Attack!");
            }
        }
    }
}
