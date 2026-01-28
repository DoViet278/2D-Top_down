using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float roamChangeDir = 2f;
    [SerializeField] private float attackRange = 0f;
    [SerializeField] private MonoBehaviour enemyType;
    [SerializeField] private bool stopMovingWhileAttacking = false;

    private bool canAttack = true;
    private float attackCooldown = 2f;
    private enum State
    {
        Roaming,
        Attacking
    }

    private State state;    
    private EnemyPathFinding pathFinding;

    private Vector2 roamingPosition;
    private float timeRoaming;  

    private void Awake()
    {
        pathFinding = GetComponent<EnemyPathFinding>();
        state = State.Roaming;
    }

    private void Start()
    {
        roamingPosition = GetRoamingPosition();
    }

    private void Update()
    {
        MovementStateControl();
    }

    private void MovementStateControl() 
    {
        switch(state)
        {
            default:
            case State.Roaming:
                Roaming();
                break;
            case State.Attacking:
                Attacking();
                break;
        }
    }


    private void Roaming()
    {
        timeRoaming += Time.deltaTime;

        pathFinding.MoveTo(roamingPosition);

        if(Vector2.Distance(transform.position, PlayerController.Instance.transform.position) < attackRange)
        {
            state = State.Attacking;
        }

        if(timeRoaming > roamChangeDir)
        {
            roamingPosition = GetRoamingPosition();
        }


    }

    private void Attacking()
    {
        if (Vector2.Distance(transform.position, PlayerController.Instance.transform.position) > attackRange)
        {
            state = State.Roaming;
        }
        if (attackRange != 0 && canAttack)
        {
            canAttack = false;
            (enemyType as IEnemy).Attack();

            if (stopMovingWhileAttacking)
            {
                pathFinding.StopMoving();
            }
            else
            {
                pathFinding.MoveTo(roamingPosition);
            }

            StartCoroutine(AtkCooldown());
        }
        
    }

    private IEnumerator AtkCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private Vector2 GetRoamingPosition()
    {
        timeRoaming = 0f;
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

}
