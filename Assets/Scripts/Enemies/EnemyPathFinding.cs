using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathFinding : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;

    private Rigidbody2D rb;
    private Vector2 moveDir;
    private Knockback knockback;
    private SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        knockback = GetComponent<Knockback>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if(!knockback.isKnockedBack)
            rb.MovePosition(rb.position + moveDir * moveSpeed * Time.fixedDeltaTime);
    
        if(moveDir.x < 0)
        {
            sr.flipX = true;
        }
        else if(moveDir.x > 0)
        {
            sr.flipX = false;
        }
    }

    public void MoveTo(Vector2 targetPosition)
    {
        moveDir = targetPosition;   
    }

    public void StopMoving()
    {
        moveDir = Vector3.zero;
    }
}
