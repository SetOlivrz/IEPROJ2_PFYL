using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public PlayerController player;
    public Transform firePoint;
    public GameObject bulletPrefab;

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

    // To fix: Shooting Direction, only shoots towards the right
    void Shoot()
    {
                                                                       // need to replace with player look at
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        Vector3 position = player.mousePos * bulletForce;
        position.y = 0;
        rb.AddForce(position, ForceMode.Impulse);
    }
}
