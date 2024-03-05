using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    public int health = 50;
    public float bulletFireCooldown = 0.25f;
    public UiController uiController;
    public GameObject bullet;
    public Texture2D cursorTexture;

    private bool inCooldown = false;

    private void Start()
    {
        Vector2 hotSpot = new Vector2(cursorTexture.width / 2f, cursorTexture.height / 2f);
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            FireGun();
        }

        CheckHealth();
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            health = 0;
            Time.timeScale = 0f;
            uiController.PlayerDied();
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
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
        health -= damage;
    }
}
