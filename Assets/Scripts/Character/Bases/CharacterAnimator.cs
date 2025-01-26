using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    protected CharacterBase characterBase;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public  CharacterAnimator Initialize(CharacterBase characterBase)
    {
        this.characterBase = characterBase;
        return this;
    }
    
    public void SetTrigger(string key)
    {
        animator.SetTrigger(key);
    }

    public void SetBool(string key, bool value)
    {
        animator.SetBool(key, value);
    }

    public void SetFloat(string key, float value)
    {
        animator.SetFloat(key, value);
    }

    public void SetInteger(string key, int value)
    {
        animator.SetInteger(key, value);
    }
}

public struct AnimationKey
{
    public const string Idle = "Idle";
    public const string IsWalking = "IsWalking";
    public const string Death = "Death";
    public const string MovementSpeed = "MovementSpeed";
}

