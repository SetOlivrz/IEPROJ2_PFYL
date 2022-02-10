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

    [SerializeField] Player playerClass;

    private void Start()
    {
        reloadImage.fillAmount = 0;
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
            if (Input.GetMouseButtonDown(0))
            {
                if(bulletCount > 0)
                {
                    Shoot();
                }
                //flash red supposedly
                /*else if (bulletCount <= 0)
                {
                    reloadImage.color = Color.red;
                    
                }*/

            }
        }

        ticks += Time.deltaTime;
        if(bulletCount < 10)
        {
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
    void Shoot()
    {
        bulletCount -= 1;

        GameObject bulletSphere = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Bullet bullet = bulletSphere.GetComponent<Bullet>();
            Vector3 playerPos = new Vector3(hit.point.x, player.transform.position.y, hit.point.z);
            bulletSphere.transform.LookAt(playerPos);
        }

    }
}
