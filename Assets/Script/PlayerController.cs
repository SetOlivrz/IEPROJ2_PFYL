using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody RB;
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject bullet;

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

    void MouseUpdate()
    {
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
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //Debug.Log(hit.point);
                Vector3 position = new Vector3(this.transform.position.x, this.transform.position.y, hit.point.z);
                Debug.Log(position);
                //something something turn to direction the thing is facing
                this.transform.LookAt(position);
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

