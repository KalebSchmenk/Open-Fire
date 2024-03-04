using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    public int health = 50;
    public float bulletFireCooldown = 0.25f;
    public GameObject bullet;

    private bool inCooldown = false;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            FireGun();
        }
    }

    private void FireGun()
    {
        if (inCooldown) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        var spawnedBullet = Instantiate(bullet, this.transform.position, Quaternion.identity, null);
        spawnedBullet.transform.up = ray.direction;
        StartCoroutine(MakeBulletAppear(spawnedBullet));

        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        inCooldown = true;
        yield return new WaitForSeconds(bulletFireCooldown);
        inCooldown = false;
    }

    // So the bullet doesnt spawn in the players face
    private IEnumerator MakeBulletAppear(GameObject bullet)
    {
        MeshRenderer bulletMesh = bullet.GetComponent<MeshRenderer>();
        bulletMesh.enabled = false;
        yield return new WaitForSeconds(0.3f);
        if (bullet != null) bulletMesh.enabled = true;
    }

    public void TakeDamage(int damage)
    {
        throw new System.NotImplementedException();
    }
}
