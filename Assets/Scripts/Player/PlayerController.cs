using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerController : Controller, IDamageable
{
    public int health = 50;
    public float bulletFireCooldown = 0.25f;
    public UiController uiController;
    public Texture2D cursorTexture;

    public AudioClip hurtSound;
    public AudioClip gunshot;
    public AudioSource gunAudioSource;

    private bool inCooldown = false;

    private Camera MainCameraRef;
    private void Start()
    {
        Vector2 hotSpot = new Vector2(cursorTexture.width / 2f, cursorTexture.height / 2f);
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
        AudioListener.volume = 0.5f;
        MainCameraRef = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            FireGun();
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
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

            AudioListener.volume = 0f;
        }
    }

    private void FireGun()
    {
        if (inCooldown) return;

        Ray ray = MainCameraRef.ScreenPointToRay(Input.mousePosition);

        gunAudioSource.PlayOneShot(gunshot);


        var spawnedBullet = Pool.Get();
        spawnedBullet.transform.position = this.transform.position;
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
        if (damage > 0) gunAudioSource.PlayOneShot(hurtSound); // If not a heal
        health -= damage;
    }
}
