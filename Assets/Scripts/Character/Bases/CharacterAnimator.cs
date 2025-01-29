using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    protected CharacterBase characterBase;
    private Animator animator;
    
    public Animator Animator => animator ??= GetComponentInChildren<Animator>();

    public  CharacterAnimator Initialize(CharacterBase characterBase)
    {
        this.characterBase = characterBase;
        Animator.transform.localPosition = Vector3.zero;
        Animator.transform.localRotation = Quaternion.identity;
        return this;
    }
    
    public void SetTrigger(string key)
    {
        Animator.SetTrigger(key);
    }

    public void SetBool(string key, bool value)
    {
        Animator.SetBool(key, value);
    }

    public void SetFloat(string key, float value)
    {
        Animator.SetFloat(key, value);
    }

    public void SetInteger(string key, int value)
    {
        Animator.SetInteger(key, value);
    }
}

public struct AnimationKey
{
    public const string Idle = "Idle";
    public const string IsRunning = "IsRunning";
    public const string Death = "Death";
    public const string MovementSpeed = "MovementSpeed";
}

