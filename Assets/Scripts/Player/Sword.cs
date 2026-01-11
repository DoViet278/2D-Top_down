using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private Transform weaponCollider;

    private PlayerControls playerControls;
    private Animator animator;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;

    private GameObject slash;

    private void Awake()
    {
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();    
        playerControls = new PlayerControls();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        playerControls.Combat.Attack.started += _ => Attack();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        MouseFollowWithOffset();
    }

    private void Attack() 
    {
        animator.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);

        slash = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slash.transform.parent = this.transform.parent;

    }

    public void DoneAtkAnimEvent() {
        weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAnimation() 
    {
        slash.gameObject.transform.rotation  = Quaternion.Euler(-180f, 0f, 0f);
        if (playerController.FacingLeft)
        {
            slash.gameObject.GetComponent<SpriteRenderer>().flipX = true;   
        }
        else
        {
            slash.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public void SwingDownFlipAnimation() 
    {
        slash.gameObject.transform.rotation  = Quaternion.Euler(0f, 0f, 0f);
        if (playerController.FacingLeft)
        {
            slash.gameObject.GetComponent<SpriteRenderer>().flipX = true;   
        }
        else
        {
            slash.gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    private void MouseFollowWithOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);

        float angle = Mathf.Atan2(mousePos.y - playerScreenPoint.y, Mathf.Abs(mousePos.x - playerScreenPoint.x)) * Mathf.Rad2Deg;

        if(mousePos.x < playerScreenPoint.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0,-180,angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0,-180,angle);
        }
        else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0,0,angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0,0,angle);
        }
    }
}
