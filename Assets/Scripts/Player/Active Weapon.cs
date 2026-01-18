using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    public MonoBehaviour CurrentActiveWeapon { get; private set; } 

    private PlayerControls playerControls;
    private float timeBetweenAttack;
    private bool atkButtonDown, isAttacking = false;

    protected override void Awake()
    {
        base.Awake();
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        playerControls.Combat.Attack.started += _ => StartAttacking();
        playerControls.Combat.Attack.canceled += _ => StopAttacking();

        AttackCooldown();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void Update()
    {
        Attack();
    }

    public void NewWeapon(MonoBehaviour newWeapon)
    {
        CurrentActiveWeapon = newWeapon;
        AttackCooldown();
        timeBetweenAttack = (CurrentActiveWeapon as IWeapon).GetWeaponInfos().countdownTime;
    }

    public void WeaponNull()
    {
        CurrentActiveWeapon = null; 
    }

    private void AttackCooldown() 
    {
        isAttacking = true;
        StopAllCoroutines();
        StartCoroutine(TimeBetweenAttackRoutine());
    }

    private IEnumerator TimeBetweenAttackRoutine()
    {
        yield return new WaitForSeconds(timeBetweenAttack);
        isAttacking = false;
    }
    private void StartAttacking()
    {
        atkButtonDown = true;
    }

    private void StopAttacking()
    {
        atkButtonDown = false;
    }

    public void ToggleIsAttacking(bool value)
    {
        isAttacking = value;    
    }

    private void Attack()
    {
        if(atkButtonDown && !isAttacking && CurrentActiveWeapon)
        {
            AttackCooldown();
            (CurrentActiveWeapon as IWeapon).Attack();
        }
    }
}
