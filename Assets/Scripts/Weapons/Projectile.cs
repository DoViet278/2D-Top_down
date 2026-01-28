using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject particleOnHit;
    [SerializeField] private bool isEnemyProjectile = false;

    [SerializeField] private float projectileRange = 10f;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;

    }
    void Update()
    {
       MoveArrow(); 
       DetectFireDistance();
    }

    public void UpdateProjectileRange(float projectileRange)
    {
        this.projectileRange = projectileRange;
    }

    public void UpdateMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        Indestructible indestructible = collision.gameObject.GetComponent<Indestructible>();
        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

        if (!collision.isTrigger &&  (enemyHealth || indestructible || playerHealth))
        {
            if((playerHealth && isEnemyProjectile) || (enemyHealth && !isEnemyProjectile))
            {
                playerHealth?.TakeDamage(1,transform);
                Instantiate(particleOnHit, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else if(indestructible && !collision.isTrigger)
            {
                Instantiate(particleOnHit, transform.position, transform.rotation);
                Destroy(gameObject);
            }

        }

    }

    private void DetectFireDistance()
    {
        if(Vector3.Distance(startPos, transform.position) > projectileRange)
        {
            Destroy(gameObject);
        }
    }

    private void MoveArrow()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
}
