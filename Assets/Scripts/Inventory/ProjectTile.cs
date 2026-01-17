using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectTile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject particleOnHit;

    private WeaponInfos weaponInfo;
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

    public void UpdateWeaponInfo(WeaponInfos info)
    {
        this.weaponInfo = info;
    }   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        Indestructible indestructible = collision.gameObject.GetComponent<Indestructible>();

        if(!collision.isTrigger &&  (enemyHealth || indestructible))
        {
            enemyHealth?.TakeDamage(weaponInfo.weaponDamage);
            Instantiate(particleOnHit, transform.position, transform.rotation);
            Destroy(gameObject);
        }

    }

    private void DetectFireDistance()
    {
        if(Vector3.Distance(startPos, transform.position) > weaponInfo.weaponRange)
        {
            Destroy(gameObject);
        }
    }

    private void MoveArrow()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
}
