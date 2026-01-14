using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool isKnockedBack {  get; private set; }

    private float knockbackDuration = .2f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void KnockBack(Transform damageSource, float knockbackForce)
    {
        isKnockedBack = true;
        Vector2 diffence = (transform.position - damageSource.position).normalized * knockbackForce * rb.mass;
        rb.AddForce(diffence,ForceMode2D.Impulse);
        StartCoroutine(KnockbackRoutine()); 
    }

    private IEnumerator KnockbackRoutine()
    {
        yield return new WaitForSeconds(knockbackDuration);
        rb.velocity = Vector2.zero;
        isKnockedBack = false;
    }

}
