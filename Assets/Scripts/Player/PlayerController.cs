using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private TrailRenderer trail;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    private bool facingLeft = false;

    //dash
    private float dashTime =.2f;
    private float dashSpeed = 4f;
    private float dashCooldown = .25f;
    private bool isDashing = false;
    private float startingMoveSpeed;

    public bool FacingLeft { get { return facingLeft; } }

    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash();    
        startingMoveSpeed = moveSpeed;
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }


    private void Update()
    {
        PlayerInput();
    }

    private void FixedUpdate()
    {
        AdjustFacingDirection();
        Move(); 
    }

    private void PlayerInput()
    {
        movement = playerControls.Movement.Move.ReadValue<Vector2>();

        animator.SetFloat("moveX", movement.x); 
        animator.SetFloat("moveY", movement.y);
    }

    private void Move()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void AdjustFacingDirection()
    {
        Vector3 mousePosition = Input.mousePosition;    
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);

        if(mousePosition.x < screenPoint.x)
        {
            sr.flipX = true;
            facingLeft = true;
        }
        else 
        { 
            sr.flipX= false;
            facingLeft = false;
        }
    }

    private void Dash() 
    {
        if(!isDashing)
        {
            isDashing = true;
            moveSpeed *= dashSpeed;
            trail.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine() 
    {
        yield return new WaitForSeconds(dashTime);  
        moveSpeed = startingMoveSpeed;
        trail.emitting = false;
        yield return new WaitForSeconds(dashCooldown);
        isDashing = false;
    }
}
