using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Lean.Pool;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    private bool isDeath;
    private float health;
    [SerializeField] private float maxHealth;
    private void OnEnable()
    {
        health = maxHealth;
        isDeath = false;
    }

    public void TakeDamage(float damageAmount)
    {
        if(isDeath) return;
        
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDeath = true;
        GetComponent<CharacterBase>().Die();
    }
}
