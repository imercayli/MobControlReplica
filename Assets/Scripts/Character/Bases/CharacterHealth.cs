using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    protected CharacterBase characterBase;
    [SerializeField] protected float maxHealth;
    protected float health;
    protected bool isDeath,isImmune;
    
    public void Initialize(CharacterBase characterBase)
    {
        this.characterBase = characterBase;
        health = maxHealth;
        isDeath = false;
    }
    
    public virtual void TakeDamage(float damageAmount)
    {
        if(isDeath || isImmune) return;
        
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDeath = true;
        characterBase.Die();
    }

    public void SetImmune(bool isImmuneActive)
    {
        isImmune = isImmune;
    }
}
