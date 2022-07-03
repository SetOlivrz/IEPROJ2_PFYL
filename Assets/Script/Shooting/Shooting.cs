using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    public PlayerController player;
    public Transform firePoint;
    public GameObject bulletPrefab;

    private int bulletCount = 10;

    [SerializeField] GameObject bulletCountUI;
    [SerializeField] GameObject bulletCountText;
    [SerializeField] Image reloadImage;
    private float ticks = 0.0f;
    private const float INTERVAL = 5f;

    Player playerClass;

    //Hand
    private GameObject defaultHand;
    private GameObject rightHand;
    private GameObject leftHand;

    //Current tool
    private ItemStack currentHeldItem;
    
    //Mobile mode
    private bool mobileMode = false;

    private void Start()
    {
        reloadImage.fillAmount = 0;
        playerClass = gameObject.GetComponent<Player>();
        mobileMode = playerClass.MobileMode;
/*        defaultHand = player.transform.GetChild(6).gameObject;
        rightHand = player.transform.GetChild(7).gameObject;
        leftHand = player.transform.GetChild(8).gameObject;*/
    }

    // Update is called once per frame
    void Update()
    {
         currentHeldItem = playerClass.myInventory.GetInventoryStacks()[playerClass.GetSelectedHotbarIndex()];

        //Equipping weapon
        if (currentHeldItem.GetItem() != null)
        {
            if (currentHeldItem.GetItem().ItemName == "Gun")
            {
                bulletCountUI.SetActive(true);
            }

            else
            {
                bulletCountUI.SetActive(false);
            }
        }
        
        if (mobileMode) return;

        if (player.isShooting)
        {
            // Mouse
            if (Input.GetMouseButtonDown(0))
            {
                //ChangeEquippedSprite();
                ChangeSlot();
                ShootRaycast();
            }
        }


    }
    /*TEMP FIX RELOADING NOT WORKING IN UPDATE*/
    private void FixedUpdate()
    {
        if (bulletCount < 10)
        {
            ticks += Time.deltaTime;
            //reloading ui
            reloadImage.fillAmount = ticks / 5.0f;
            if (ticks > INTERVAL)
            {
                bulletCount++;
                ticks = 0.0f;
                reloadImage.fillAmount = 0;
            }
        }

        bulletCountText.GetComponent<Text>().text = bulletCount.ToString();
    }
    public void ShootRaycast()
    {
        if (currentHeldItem.GetItem() != null)
            if (currentHeldItem.GetItem().ItemName != "Gun")
                return;

        if (bulletCount <= 0)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            GameObject bulletSphere = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bulletSphere.transform.LookAt(new Vector3(hit.point.x, 0.5f, hit.point.z));
            bulletCount -= 1;
            
            Animator animator = this.GetComponent<Animator>();
            //Debug.Log($"Hit Point: {hit.point} & Transform: {transform.position}");

            /*if (hit.point.z > transform.position.z)
            {
                animator.SetBool("back", true);
            }
            if (hit.point.z < transform.position.z)
            {
                animator.SetBool("front", true);
            }
            if (hit.point.x > transform.position.x)
            {
                animator.SetBool("right", true);
            }
            else if (hit.point.x < transform.position.x)
            {
                animator.SetBool("left", true);
            }*/
        }
    }

    public void ShootInDirection(Vector2 direction)
    {
        if (currentHeldItem.GetItem() != null)
            if (currentHeldItem.GetItem().ItemName != "Gun")
                return;

        if (bulletCount <= 0)
            return;

        GameObject bulletSphere = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        //bulletSphere.transform.LookAt(new Vector3(direction.x, 0.5f, direction.y));
        Vector3 dir3d = new Vector3(-direction.x, 0.5f, -direction.y);
        bulletSphere.transform.rotation = Quaternion.LookRotation(dir3d);
        bulletCount -= 1;
    }

    void ChangeSlot()
    {
        /*if (player.isRight)
        {
            defaultHand.SetActive(false);
            rightHand.SetActive(true);
            leftHand.SetActive(false);
        }

        else if (!player.isRight)
        {
            defaultHand.SetActive(false);
            rightHand.SetActive(false);
            leftHand.SetActive(true);
        }

        else if (player.isUpwards || !player.isUpwards)
        {
            defaultHand.SetActive(true);
            rightHand.SetActive(false);
            leftHand.SetActive(false);
        }*/

        StartCoroutine("DisableItem");
    }

    public IEnumerator DisableItem()
    {
        yield return new WaitForSeconds(0.3f);
        EmptyHand();
    }

    void EmptyHand()
    {
        ItemStack currentHeldItem = playerClass.myInventory.GetInventoryStacks()[playerClass.GetSelectedHotbarIndex()];
        /*defaultHand.SetActive(false);
        rightHand.SetActive(false);
        leftHand.SetActive(false);*/
    }
}
