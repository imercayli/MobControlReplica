using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class CharacterInteraction : MonoBehaviour
{
    protected bool isInteractionActive;
    [SerializeField] private Collider characterCollider;
    public BoostGate createdBoostGate;

    protected virtual void Start()
    {
        isInteractionActive = false;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(!isInteractionActive) return;
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.TryGetComponent(out Enemy enemy))
        {
            Debug.Log(5);
            GetComponent<CharacterHealth>().TakeDamage(1);
            enemy.GetComponent<CharacterHealth>().TakeDamage(1);
        }
    }

    public void SetInteractionActivation(bool isActive)
    {
        isInteractionActive = isActive;
    }
}
