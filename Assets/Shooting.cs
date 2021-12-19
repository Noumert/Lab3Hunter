using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    public Transform firePoint;
    public int bulletsMaxAmount = 50;
    public GameObject bulletPrefab;
    public float bulletForce = 20f;
    public float range = 20f;
    public Text bulletsText;
    private int bulletsAmount = 50;
    private List<GameObject> bullets;

    private void Start()
    {
        bulletsAmount = bulletsMaxAmount;
        bullets = new List<GameObject>();
        bulletsText.text = bulletsAmount + "/" + bulletsMaxAmount;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (bulletsAmount > 0)
            {
                bulletsAmount-= 1;
                 Shoot();
                 bulletsText.text = bulletsAmount + "/" + bulletsMaxAmount;
            }
        }

        ClearBulletsByRange();
    }

    private void ClearBulletsByRange()
    {
        if (bullets.Count == 0)
        {
            return;
        }
        
        foreach (int bulletI in Enumerable.Range(0,bullets.Count))
        {
            if (bullets.ElementAt(bulletI).IsDestroyed())
            {
                bullets.RemoveAt(bulletI);
                return;
            }
            float bulletRange = Vector2.Distance(bullets.ElementAt(bulletI)
                .transform.position, firePoint.position);
            if (bulletRange >= range)
            {
                Destroy(bullets.ElementAt(bulletI));
                bullets.RemoveAt(bulletI);
                return;
            }
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        bullets.Add(bullet);
    }
}