using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody RB;
    [SerializeField] float moveSpeed;
    [SerializeField] Player player;

    //checker to see if the player can shoot
    public bool isShooting = false;
    public bool isUpwards = false;
    public bool isRight = false;

    [SerializeField] Animator animator;

    private Vector2 moveInput;
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        UpdateMoveAnimation();
        AnimationChecker();
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

    void MouseUpdate()
    {
        //will change checker once items are implemented
        if (!isShooting && player.myInventory.GetInventoryStacks()[player.GetSelectedHotbarIndex()].GetItem() is Tool gun && gun.GetToolType() == Tool.ToolTypes.Gun)
        {
            isShooting = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            ShootHandler();
        }

        if (isShooting && !(player.myInventory.GetInventoryStacks()[player.GetSelectedHotbarIndex()].GetItem() is Tool notGun && notGun.GetToolType() == Tool.ToolTypes.Gun))
        {
            isShooting = false;
        }
    }

    private void AnimationChecker()
    {
        //W
        if (animator.GetBool("back"))
        {
            isUpwards = true;
            isRight = false;
        }
        //S
        else if (animator.GetBool("front"))
        {
            isUpwards = false;
            isRight = false;
        }
        //A
        else if (animator.GetBool("left"))
        {
            isUpwards = false;
            isRight = false;
        }
        //D
        else if (animator.GetBool("right"))
        {
            isUpwards = false;
            isRight = true;
        }
    }

    //Handles the shooting for the player
    void ShootHandler()
    {
        if (isShooting)
        {
            animator.SetTrigger("Shoot");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                if (hit.point.z > transform.position.z)
                {
                    ResetBool();
                    animator.SetBool("back", true);
                }
                if (hit.point.z < transform.position.z)
                {
                    ResetBool();
                    animator.SetBool("front", true);
                }
                if (hit.point.x > transform.position.x)
                {
                    ResetBool();
                    animator.SetBool("right", true);
                }
                else if (hit.point.x < transform.position.x)
                {
                    ResetBool();
                    animator.SetBool("left", true);
                }
            }
        }
    }
}

