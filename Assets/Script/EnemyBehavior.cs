using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour
{
    //Target
    protected Transform player;
    private bool reachedPlayer = false;

    //Enemy Stats
    [SerializeField] private string enemyName;
    [SerializeField] private float speed = 0;
    public float health = 50.0f;
    public float damage = 5.0f;
    
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

    //Drops Copy
    [SerializeField] private GameObject drop;

    private PlayerData playerData;
    // count for dead enemies
    public int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        
       player = GameObject.FindGameObjectWithTag("Player").transform;
       prev_x = transform.position.x;
       prev_z = transform.position.z;
       speed = Random.Range(0.5f, 3.0f);

       playerData = player.GetComponent<PlayerData>();

        if (playerData == null)
        {
            Debug.Log("Component not Found");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!reachedPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

            if (this.enemyName == "Normal Golem") UpdateSprite();
        }
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
        if(this.health <= 0)
        {
            OnKill();
        }
    }

    private void OnCollisionStay(Collision collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            this.ticks += Time.deltaTime;
            if (ticks > ATTACK_INTERVAL)
            {
                ticks = 0.0f;

                //player.health -= damage;
                playerData.TakeDamage(damage);
                
                
                Debug.Log("Attack!");
            }
        }
    }

    private void OnCollisionEnter(Collision collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            reachedPlayer = true;
        }
    }

    private void OnCollisionExit(Collision collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            reachedPlayer = false;
        }
    }

    public void OnKill()
    {
        //dropItem = true;
        playerData.addGold(10);
        count++;
        Instantiate(drop, transform.position, transform.rotation);
        Destroy(this.gameObject);
        
        
    }
}
