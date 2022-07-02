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

    //Hand
    private GameObject defaultHand;
    private GameObject rightHand;
    private GameObject leftHand;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<Player>();
        defaultHand = player.transform.GetChild(3).gameObject;
        rightHand = player.transform.GetChild(4).gameObject;
        leftHand = player.transform.GetChild(5).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        UpdateMoveAnimation();
        AnimationChecker();
        MouseUpdate();
        ChangeSlot();
        ChangeEquippedSprite();
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

    void ChangeEquippedSprite()
    {
        ItemStack currentHeldItem = player.myInventory.GetInventoryStacks()[player.GetSelectedHotbarIndex()];

        if (currentHeldItem.GetItem() != null)
        {
            defaultHand.GetComponent<SpriteRenderer>().sprite = currentHeldItem.item.ItemIcon;
            rightHand.GetComponent<SpriteRenderer>().sprite = currentHeldItem.item.ItemIcon;
            leftHand.GetComponent<SpriteRenderer>().sprite = currentHeldItem.item.ItemIcon;

            if (currentHeldItem.item.ItemName == "Rose Sword" || currentHeldItem.item.ItemName == "Gun")
            {
                //Hand
                defaultHand.SetActive(false);
                rightHand.SetActive(false);
                leftHand.SetActive(false);
            }
        }
        else
        {
            defaultHand.GetComponent<SpriteRenderer>().sprite = null;
            rightHand.GetComponent<SpriteRenderer>().sprite = null;
            leftHand.GetComponent<SpriteRenderer>().sprite = null;
        }
    }

    void EmptyHand()
    {
        ItemStack currentHeldItem = player.myInventory.GetInventoryStacks()[player.GetSelectedHotbarIndex()];
        defaultHand.SetActive(false);
        rightHand.SetActive(false);
        leftHand.SetActive(false);
    }

    void ChangeSlot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StopCoroutine("DisableItem");
            if (isRight)
            {
                defaultHand.SetActive(false);
                rightHand.SetActive(true);
                leftHand.SetActive(false);
                //EmptyHand();
            }

            else if (!isRight)
            {
                defaultHand.SetActive(false);
                rightHand.SetActive(false);
                leftHand.SetActive(true);
                //EmptyHand();
            }

            else if (isUpwards || !isUpwards)
            {
                defaultHand.SetActive(true);
                rightHand.SetActive(false);
                leftHand.SetActive(false);
                //EmptyHand();
            }

            StartCoroutine("DisableItem");
        }
    }

    public IEnumerator DisableItem()
    {
        yield return new WaitForSeconds(0.3f);
        EmptyHand();
    }
}

