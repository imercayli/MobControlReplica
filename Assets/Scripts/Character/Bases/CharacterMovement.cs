using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterMovement : MonoBehaviour
{
    protected CharacterBase characterBase;
    [SerializeField] protected NavMeshAgent navMeshAgent;
    [SerializeField] private float speed;
    private bool isMovementActive;

    private void OnEnable()
    {
        navMeshAgent.speed = speed;
        SetMovementActivation(true);
        
    }

    public void SetTraget(Vector3 target)
    { 
        target.x = transform.position.x;//todo
         navMeshAgent.SetDestination(target);
        GetComponent<CharacterAnimator>().SetBool(AnimationKey.IsWalking,true);

        StartCoroutine(Routine());
        IEnumerator Routine()
        {
            navMeshAgent.speed *= 1.5f;
            yield return new WaitForSeconds(.2f);
            navMeshAgent.speed /= 1.5f;
        }
    }

    void Update()
    {
       // Move();
      
    }

    // void FixedUpdate()
    // {
    //     Vector3 newPosition = GetComponent<Rigidbody>().position + transform.forward * speed * Time.fixedDeltaTime;
    //     GetComponent<Rigidbody>().MovePosition(newPosition);
    //     GetComponent<CharacterAnimator>().SetBool(AnimationKey.IsWalking,true);
    // }

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
         navMeshAgent.enabled = isActive;
    }
}