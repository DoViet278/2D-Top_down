using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : Singleton<PlayerHealth>
{
    public bool isDead { get; private set; }   

    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;

    private Slider healthSlider;
    private Knockback knockback;
    private Flash flash;
    private int currentHealth;
    private bool canTakeDamage = true;
    readonly int DEATH_ANIMATION_HASH = Animator.StringToHash("Death");

    protected override void Awake()
    {
        base.Awake();

        knockback = GetComponent<Knockback>();  
        flash = GetComponent<Flash>();
        currentHealth = maxHealth;
    }

    private void Start()
    {
        isDead = false; 
        UpdateHealthSlider();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        EnemyAI emeny = collision.gameObject.GetComponent<EnemyAI>();

        if (emeny)
        {
            TakeDamage(1,collision.gameObject.transform);  
          
        }
    }

    public void HealPlayer()
    {
        if(currentHealth < maxHealth)
        {
            currentHealth += 1;
            UpdateHealthSlider();
        }
    }

    public void TakeDamage(int damage, Transform hitTransform)
    {
        if(!canTakeDamage) return;  

        canTakeDamage = false;
        ScreenShakeManager.Instance.ShakeScreen();
        currentHealth -= damage;
        knockback.KnockBack(hitTransform, knockBackThrustAmount);
        flash.FlashSprite();
        StartCoroutine(DamageRecoveryRoutine());
        UpdateHealthSlider();
        CheckPlayerDeath();
    }

    private void CheckPlayerDeath()
    {
        if(currentHealth <= 0 && !isDead)
        {
            isDead=true;
            Destroy(ActiveWeapon.Instance.gameObject);
            currentHealth = 0;
            GetComponent<Animator>().SetTrigger(DEATH_ANIMATION_HASH);
            StartCoroutine(DeathLoadSceneRoutine());
        }
    }

    private IEnumerator DeathLoadSceneRoutine()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
        Stamina.Instance.ReplenishStaminaOnDeath();
        SceneManager.LoadScene("Town");
    }

    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);    
        canTakeDamage = true;
    }

    private void UpdateHealthSlider()
    {
        if(healthSlider == null)
        {
            healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
        }

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;

    }
}
