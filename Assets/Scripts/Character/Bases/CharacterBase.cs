using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{ 
    private GameService gameService;
    
    private CharacterMovement characterMovement;
    private CharacterAttack characterAttack;
    private CharacterHealth characterHealth;
    private CharacterInteraction characterInteraction;
    private CharacterAnimator characterAnimator;

    private Color orjinColor;
    [SerializeField] private Color deathColor;
    [SerializeField] private SkinnedMeshRenderer characterSkinnedMeshRenderer;

    public GameService GameService => gameService ??= ServiceSystem.GetService<GameService>();
    public CharacterMovement CharacterMovement => characterMovement ??= GetComponent<CharacterMovement>();
    public CharacterAttack CharacterAttack => characterAttack ??= GetComponent<CharacterAttack>();
    public CharacterHealth CharacterHealth => characterHealth ??= GetComponent<CharacterHealth>();
    public CharacterInteraction CharacterInteraction => characterInteraction ??= GetComponent<CharacterInteraction>();
    public CharacterAnimator CharacterAnimator => characterAnimator ??= GetComponent<CharacterAnimator>();

    public Action OnDie;

    protected virtual void Awake()
    {
        orjinColor = characterSkinnedMeshRenderer.material.color;
    }

    protected virtual void OnEnable()
    {
        CharacterMovement.Initialize(this);
        CharacterAttack.Initialize(this);
        CharacterHealth.Initialize(this);
        CharacterInteraction.Initialize(this);
        CharacterAnimator.Initialize(this);
        characterSkinnedMeshRenderer.material.color = orjinColor;
    }

    public virtual void Die()
    {
        OnDie?.Invoke();
        CharacterAnimator.SetTrigger(AnimationKey.Death);
        characterSkinnedMeshRenderer.material.DOColor(deathColor, .2f);
        transform.DOMoveY(transform.position.y - 1f, 1.5f).OnComplete((() =>
        {
            LeanPool.Despawn(this);
        }));
       
    }
}