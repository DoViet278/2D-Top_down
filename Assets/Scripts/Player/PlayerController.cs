using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] private float moveSpeed = 1f;

    private PlayerControls playerControls;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    private bool facingLeft = false;

    public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }

    private void Awake()
    {
        instance = this;
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
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
            FacingLeft = true;
        }
        else 
        { 
            sr.flipX= false;
            FacingLeft = false;
        }
    }
}
