using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    private CharacterMovement characterMovement;
    private CharacterHealth characterHealth;
    private CharacterInteraction characterInteraction;
    private CharacterAnimator characterAnimator;
    private Color orjinColor;
    [SerializeField] private Color deathColor;

    public CharacterMovement CharacterMovement => characterMovement ??= GetComponent<CharacterMovement>();
    public CharacterHealth CharacterHealth => characterHealth ??= GetComponent<CharacterHealth>();
    public CharacterInteraction CharacterInteraction => characterInteraction ??= GetComponent<CharacterInteraction>();
    public CharacterAnimator CharacterAnimator => characterAnimator ??= GetComponent<CharacterAnimator>();

    public Action OnDie;
    private bool isREspawn;
    
    protected virtual void Start()
    {
        orjinColor = GetComponentInChildren<SkinnedMeshRenderer>().material.color;
    }

    private void OnEnable()
    {
        if(isREspawn)
          GetComponentInChildren<SkinnedMeshRenderer>().material.color = orjinColor;
    }

    public virtual void Die()
    {
        isREspawn = true;
        GetComponentInChildren<SkinnedMeshRenderer>().material.DOColor(deathColor, 1f);
        OnDie?.Invoke();
        CharacterMovement.SetMovementActivation(false);
        CharacterInteraction.SetInteractionActivation(false);
        CharacterAnimator.SetTrigger(AnimationKey.Death);
        transform.DOMoveY(transform.position.y - 2, 3f).OnComplete((() =>
        {
            LeanPool.Despawn(this);
        }));
    }
    
}