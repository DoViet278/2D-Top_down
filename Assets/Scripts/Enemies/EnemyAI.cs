using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    [SerializeField] private float roamChangeDir = 2f;
    private enum State
    {
        Roaming
    }

    private State state;    
    private EnemyPathFinding pathFinding;

    private void Awake()
    {
        pathFinding = GetComponent<EnemyPathFinding>();
        state = State.Roaming;
    }

    private void Start()
    {
        StartCoroutine(RoamingRoutine());   
    }

    private IEnumerator RoamingRoutine()
    {
        while(state == State.Roaming)
        {
            Vector2 roamPosition = GetRoamingPosition();
            pathFinding.MoveTo(roamPosition);
            yield return new WaitForSeconds(roamChangeDir);
        }
    }

    private Vector2 GetRoamingPosition() => new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

}
