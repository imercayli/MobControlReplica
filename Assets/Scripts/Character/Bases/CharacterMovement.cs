using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

//[RequireComponent(typeof(NavMeshAgent))]
public class CharacterMovement : MonoBehaviour
{
    protected CharacterBase characterBase;
   // [SerializeField] protected NavMeshAgent navMeshAgent;
    [SerializeField] private float speed;
    private bool isMovementActive;
    private GameService gameService;
    private SoundService soundService;

    protected float xOffset;

    public void Initialize(CharacterBase characterBase)
    {
        this.characterBase = characterBase;
        characterBase.OnDie += () => { SetMovementActivation(false); };
        this.characterBase.GameService.OnGameOver += (isSuccess) => { SetMovementActivation(false); };
       // navMeshAgent.speed = speed;
        SetMovementActivation(true);
        gameService = ServiceSystem.GetService<GameService>();
        soundService =ServiceSystem.GetService<SoundService>();
    }

    private void Update()
    {
        MoveWithTransform();
    }

    private void MoveWithTransform()
    {
        if(!isMovementActive) return;
        
        transform.position = Vector3.Lerp(transform.position, 
            transform.position + transform.forward + transform.right*xOffset,
            Time.deltaTime * speed);
    }
    public void SetXOffset(int direction)
    {
        StartCoroutine(Routine());
        IEnumerator Routine()
        {
            float magnitude = 0.2f;
            xOffset = direction *magnitude;
            yield return new WaitForSeconds(0.25f);
            xOffset = 0;
        }
    }

    public void SetTargetForward(Vector3 target, bool isFaster = false)
    {
        Vector3 forwardTarget = target;
        forwardTarget.x = transform.position.x;
        SetTarget(forwardTarget,isFaster);
    }
    
    
    public void SetTarget(Vector3 target, bool isFaster = false)
    {
        // if (!navMeshAgent.enabled || !navMeshAgent.isOnNavMesh) return;
        //
        //  navMeshAgent.SetDestination(target);
        // characterBase.CharacterAnimator.SetBool(AnimationKey.IsRunning, true);
        //
        // if (!gameService.IsGameOver)
        //     soundService.PlaySound("CharacterRun");
        //     
        //
        // if (isFaster)
        // {
        //     StartCoroutine(Routine());
        //
        //     IEnumerator Routine()
        //     {
        //         navMeshAgent.speed *= 2f;
        //         yield return new WaitForSeconds(.2f);
        //         navMeshAgent.speed /= 2f;
        //     }
        // }
    }
    
    public void SetMovementActivation(bool isActive)
    {
        isMovementActive = isActive;
      //  navMeshAgent.enabled = isActive;
        characterBase.CharacterAnimator.SetBool(AnimationKey.IsRunning, isActive);
    }

    private void OnDisable()
    {
        if (characterBase)
        {
            characterBase.OnDie -= () => { SetMovementActivation(false); };
            characterBase.GameService.OnGameOver -= (isSuccess) => { SetMovementActivation(false); };
        }
       
    }
}