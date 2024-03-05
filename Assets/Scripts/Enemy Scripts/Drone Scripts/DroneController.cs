using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour, IDamageable
{
    public Transform bulletSpawnPos;
    public GameObject bullet;
    public Vector2 shootEvery = new Vector2(1, 5);
    public Animator animator;
    public Rigidbody rb;

    public GameObject explosion;
    public float health = 75f;
    [NonSerialized] public bool isDead = false;


    // Movement Vars
    [SerializeField] private float bounceSpeed = 10f;
    [SerializeField] private float bounceHeight = 1.5f;
    private float maxYVal;
    private float startYVal;
    private bool moveUp = true;
    private Vector3 vectorDown = new Vector3(0f, -1f, 0);
    private Vector3 vectorUp = new Vector3(0f, 1f, 0);

    private void Start()
    {
        startYVal = transform.position.y;
        maxYVal = startYVal + bounceHeight;

        bounceSpeed *= 0.0025f; // Bounce speed is really strong even if its a small number, multiply by this literal so it's not so strong
    }

    private void OnEnable()
    {
        OpenFire();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void Update()
    {
        if (isDead) return;

        CheckHealth();
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            this.transform.parent = null;
            rb.useGravity = true;
            Destroy(Instantiate(explosion, this.transform.position, Quaternion.identity), 10f); // Create explosion and destroy it in given time
            isDead = true;
            animator.SetTrigger("Die");
            StopAllCoroutines();

            GameManager.EnemyKilled?.Invoke();
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    private void OpenFire()
    {
        StartCoroutine(RepeatShoot());
    }

    private IEnumerator RepeatShoot()
    {
        Transform playerTrans = GameManager.instance.player.transform;

        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(shootEvery.x, shootEvery.y));

            var spawnedBullet = Instantiate(bullet, this.transform.position, Quaternion.identity, null);

            var dir = playerTrans.position - this.transform.position;

            spawnedBullet.transform.up = dir;
        }  
    }

    #region Bounce
    void FixedUpdate()
    {
        if (isDead) return;

        if (moveUp) { MoveUp(); } else { MoveDown(); } // Ternary operator can't be used so this is next best thing
    }

    private void MoveUp()
    {
        transform.Translate(vectorUp * bounceSpeed);

        if (this.transform.position.y > maxYVal) moveUp = false;
    }

    private void MoveDown()
    {
        transform.Translate(vectorDown * bounceSpeed);

        if (this.transform.position.y < startYVal) moveUp = true;
    }
    #endregion
}
