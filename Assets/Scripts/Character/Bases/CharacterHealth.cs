using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    protected CharacterBase characterBase;
    [SerializeField] private float maxHealth;
    private float health;
    private bool isDeath,isImmune;
    
    public void Initialize(CharacterBase characterBase)
    {
        this.characterBase = characterBase;
        health = maxHealth;
        isDeath = false;
    }
    
    public void TakeDamage(float damageAmount)
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

    public void SetImmune(bool isImmune)
    {
        this.isImmune = isImmune;
    }
}
