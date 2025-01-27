using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterMovement : MonoBehaviour
{
    protected CharacterBase characterBase;
    protected NavMeshAgent navMeshAgent;
    [SerializeField] private float speed;
    private bool isMovementActive;

    private void OnEnable()
    {
       
        isMovementActive = true;
        
    }

    public void SetTraget(Vector3 target)
    { 
        navMeshAgent = GetComponent<NavMeshAgent>();
        target.x = transform.position.x;//todo
         navMeshAgent.SetDestination(target);
        GetComponent<CharacterAnimator>().SetBool(AnimationKey.IsWalking,true);
    }

    void Update()
    {
       // Move();
      
    }

    private void Move()
    {
        if (!isMovementActive) return;

        Vector3 newPos = transform.position + transform.forward;
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * speed);
        GetComponent<CharacterAnimator>().SetBool(AnimationKey.IsWalking,true);
    }

    public void SetMovementActivation(bool isActive)
    {
        isMovementActive = isActive;
    }
}