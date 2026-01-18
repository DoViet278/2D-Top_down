using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    private int damageAmount = 1;

    private void Start()
    {
        MonoBehaviour currentWeapons = ActiveWeapon.Instance.CurrentActiveWeapon;
        damageAmount = (currentWeapons as IWeapon).GetWeaponInfos().weaponDamage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        enemyHealth?.TakeDamage(damageAmount);
    }
}
