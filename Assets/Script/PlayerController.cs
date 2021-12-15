using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody RB;
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject bullet;

    public Inventory inventory;
    //checker to see if the player can shoot
    public bool isShooting = false;

    bool mforward;
    bool mback;
    bool mleft;
    bool mright;
    [SerializeField] Animator animator;
    public Vector3 mousePos;
    public Camera cam;

    private Vector2 moveInput;
    // Start is called before the first frame update
    void Start()
    {
        //inventory.ItemUsed += Inventory_ItemUsed;
    }

    private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {
        IInventoryItem item = e.Item;

        //Do something

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        UpdateMoveAnimation();
        MouseUpdate();            
    }

    private void UpdateMoveAnimation()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            ResetBool();
            animator.SetBool("back", true);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            ResetBool();
            animator.SetBool("left", true);

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            ResetBool();
            animator.SetBool("right", true);

        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            ResetBool();
            animator.SetBool("front", true);
        }
    }

    private void Move()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        RB.velocity = new Vector3(moveInput.x * moveSpeed, RB.velocity.y, moveInput.y * moveSpeed);
    }

    public void ResetBool()
    {
        animator.SetBool("back", false);
        animator.SetBool("front", false);
        animator.SetBool("left", false);
        animator.SetBool("right", false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IInventoryItem item = collision.collider.GetComponent<IInventoryItem>();
        if (item != null)
        {
            inventory.AddItem(item);
        }
    }

    void MouseUpdate()
    {
        mousePos = cam.ScreenToViewportPoint(Input.mousePosition);
        //will change checker once items are implemented
        if (!isShooting && Input.GetKeyDown(KeyCode.Alpha1))
        {
            isShooting = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            ShootHandler();
        }
        
    }
    //To Fix
    void ShootHandler()
    {
        if (isShooting)
        {
            if (mousePos.x == 0.5 && mousePos.y > 0.5)
            {
                ResetBool();
                animator.SetBool("back", true);
            }
            else if (mousePos.x == 0.5 && mousePos.y < 0.5)
            {
                ResetBool();
                animator.SetBool("front", true);
            }
            else if (mousePos.x >= 0.5)
            {
                ResetBool();
                animator.SetBool("right", true);
            }
            else if (mousePos.x < 0.5)
            {
                ResetBool();
                animator.SetBool("left", true);
            }
        }

        //will change soon once items are implemented
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isShooting = false;
            Debug.Log("Not Shooting");
        }
    }
}

