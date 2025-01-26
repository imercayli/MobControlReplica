using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    protected CharacterMovement _characterMovement;
    [SerializeField] private Color deathColor;

    public Action OnDie;
    
    protected virtual void Start()
    {
        
    }

    public virtual void Die()
    {
        GetComponentInChildren<SkinnedMeshRenderer>().material.DOColor(deathColor, 1f);
        OnDie?.Invoke();
        GetComponent<CharacterMovement>().SetMovementActivation(false);
        GetComponent<CharacterInteraction>().SetInteractionActivation(false);
        GetComponent<CharacterAnimator>().SetTrigger(AnimationKey.Death);
        transform.DOMoveY(transform.position.y - 2, 3f).OnComplete((() =>
        {
            LeanPool.Despawn(this);
        }));
    }
    
}