using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody RB;
    [SerializeField] float moveSpeed;

    //checker to see if the player can shoot
    public bool isShooting = false;
    public bool isUpwards = false;
    public bool isRight = false;

    [SerializeField] Animator animator;
    public Vector3 mousePos;
    public Camera cam;

    private Vector2 moveInput;
    // Start is called before the first frame update
    void Start()
    {
        
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
        if (!isShooting && Input.GetKeyDown(KeyCode.Alpha5))
        {
            isShooting = true;
        }
        if (Input.GetMouseButtonDown(0))
        {
            ShootHandler();
        }
        //will change soon once items are implemented
/*        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isShooting = false;
            Debug.Log("Not Shooting");
        }*/
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

    //To Fix
    void ShootHandler()
    {
        if (isShooting)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //Debug.Log(hit.point);
                //Debug.Log("Position: " + transform.position);
                //TO FIX
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

