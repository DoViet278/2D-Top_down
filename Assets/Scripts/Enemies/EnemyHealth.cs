using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;

    private int currentHealth;
    private Knockback knockback;

    private void Awake()
    {
        currentHealth = maxHealth;
        knockback = GetComponent<Knockback>();  
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        knockback.KnockBack(PlayerController.instance.transform, 15f);
        Die();
    }

    private void Die()
    {
        if(currentHealth <= 0)
            Destroy(gameObject);
    }
}
