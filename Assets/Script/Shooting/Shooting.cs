using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public PlayerController player;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public GameObject target;
    public float bulletForce = 20f;

    // Update is called once per frame
    void Update()
    {
        if (player.isShooting)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }
    }
    void Shoot()
    {

        GameObject bulletSphere = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletSphere.GetComponent<Bullet>();
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            Vector3 playerPos = new Vector3(hit.point.x, player.transform.position.y, hit.point.z);
            bulletSphere.transform.LookAt(playerPos);
            if (bullet != null)
            {
                bullet.Seek();
            }
        }

    }
}
