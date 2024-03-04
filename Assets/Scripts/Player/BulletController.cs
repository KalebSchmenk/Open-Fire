using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bulletSpeed = 25f;
    public int bulletDamage = 15;

    private void Start()
    {
        Destroy(this.gameObject, 15f); // If 15 seconds have passed just destroy the bullet
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.Translate(Vector3.up * bulletSpeed * 0.05f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            damageable.TakeDamage(bulletDamage);
        }

        Destroy(this.gameObject);
    }
}
