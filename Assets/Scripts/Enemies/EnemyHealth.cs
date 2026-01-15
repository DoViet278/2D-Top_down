using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private GameObject deathVfxPrefab;
    [SerializeField] private float knockBackThrust = 15f;

    private int currentHealth;
    private Knockback knockback;
    private Flash flash;

    private void Awake()
    {
        currentHealth = maxHealth;
        knockback = GetComponent<Knockback>(); 
        flash = GetComponent<Flash>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        knockback.KnockBack(PlayerController.Instance.transform, knockBackThrust);
        flash.FlashSprite();
        StartCoroutine(CheckDieRoutine());  
    }

    private IEnumerator CheckDieRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        CheckDie();
    }

    private void CheckDie()
    {
        if(currentHealth <= 0)
        {
            Instantiate(deathVfxPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
