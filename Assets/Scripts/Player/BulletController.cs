using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed = 25f;
    public int bulletDamage = 15;

    public bool isEnemyBullet = false;
    public Controller controller;
    public void Start()
    {
        Invoke(nameof(RemoveFromPoolInTime), 15f) ; // If 15 seconds have passed just destroy the bullet
    }

    public void RemoveFromPoolInTime()
    {
        controller.Pool.Release(this.gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.Translate(Vector3.up * bulletSpeed * 0.05f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isEnemyBullet)
        {
            other.gameObject.CompareTag("Cart");

            if (GameManager.instance.player.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                damageable.TakeDamage(bulletDamage);
            }
        }
        else if (other.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.TakeDamage(bulletDamage);
        }

        //Destroy(this.gameObject);
        controller.Pool.Release(this.gameObject);
        CancelInvoke(nameof(RemoveFromPoolInTime));
    }
}
