using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
    public Animator animator;
    public Rigidbody rb;

    public GameObject explosion;
    public float health = 15f;
    private bool isDead = false;


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

    private void Update()
    {
        if (isDead) return;

        CheckHealth();
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            rb.useGravity = true;
            Destroy(Instantiate(explosion, this.transform.position, Quaternion.identity), 10f); // Create explosion and destroy it in given time
            isDead = true;
            animator.SetTrigger("Die");
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
