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

    private void Start()
    {
        reloadImage.fillAmount = 0;
        playerClass = gameObject.GetComponent<Player>();
        defaultHand = player.transform.GetChild(6).gameObject;
        rightHand = player.transform.GetChild(7).gameObject;
        leftHand = player.transform.GetChild(8).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        ItemStack currentHeldItem = playerClass.myInventory.GetInventoryStacks()[playerClass.GetSelectedHotbarIndex()];

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
        
        if (player.isShooting)
        {
            // Mouse
            if (Input.GetMouseButtonDown(0))
            {
                //ChangeEquippedSprite();
                ChangeSlot();
                if (bulletCount > 0)
                {   
                    Shoot();
                }
            }
            // Android Touch
            else
            {
                if (Input.touchCount == 0) return;

                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    Shoot();
                }
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
            Debug.Log($"Reloading: {ticks}");
            reloadImage.fillAmount = ticks / 5.0f;
            if (ticks > INTERVAL)
            {
                Debug.Log("Bullet Reloaded");
                bulletCount++;
                ticks = 0.0f;
                reloadImage.fillAmount = 0;
            }
        }

        bulletCountText.GetComponent<Text>().text = bulletCount.ToString();
    }
    void Shoot()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground")))
        {
            GameObject bulletSphere = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Bullet bullet = bulletSphere.GetComponent<Bullet>();
            Vector3 playerPos = new Vector3(hit.point.x, player.transform.position.y, hit.point.z);
            bulletSphere.transform.LookAt(playerPos);
            bulletCount -= 1;
        }
    }

    void ChangeSlot()
    {
        if (player.isRight)
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
        }

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
        defaultHand.SetActive(false);
        rightHand.SetActive(false);
        leftHand.SetActive(false);
    }
}
