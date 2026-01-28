using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIdleAnimation : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();    
    }

    private void Start()
    {
        if (!animator) return;

        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
        animator.Play(currentState.fullPathHash, -1, Random.Range(0f, 1f)); 
    }
}
