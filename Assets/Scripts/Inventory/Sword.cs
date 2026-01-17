using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashAnimPrefab;
    [SerializeField] private Transform slashAnimSpawnPoint;
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private WeaponInfos weaponInfo;   

    private Transform weaponCollider;
    private Animator animator;
    private GameObject slash;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
        slashAnimSpawnPoint = GameObject.Find("SlashAnimSpawnPoint").transform;
    }

    private void Update()
    {
        MouseFollowWithOffset();  
    }

    public void Attack() 
    {
        animator.SetTrigger("Attack");
        weaponCollider.gameObject.SetActive(true);
        slash = Instantiate(slashAnimPrefab, slashAnimSpawnPoint.position, Quaternion.identity);
        slash.transform.parent = this.transform.parent;
    }

    public WeaponInfos GetWeaponInfos() 
    {
        return weaponInfo;
    }

    public void DoneAtkAnimEvent() {
        weaponCollider.gameObject.SetActive(false);
    }

    public void SwingUpFlipAnimation() 
    {
        slash.gameObject.transform.rotation  = Quaternion.Euler(-180f, 0f, 0f);
        if (PlayerController.Instance.FacingLeft)
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
        if (PlayerController.Instance.FacingLeft)
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
        Vector3 playerScreenPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y - playerScreenPoint.y, Mathf.Abs(mousePos.x - playerScreenPoint.x)) * Mathf.Rad2Deg;

        if(mousePos.x < playerScreenPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0,-180,angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0,-180,angle);
        }
        else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0,0,angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0,0,angle);
        }
    }
}
