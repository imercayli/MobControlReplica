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
    
    private void OnEnable()
    {
        isInteractionActive = true;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(!isInteractionActive) return;
        
    }
    public void SetInteractionActivation(bool isActive)
    {
        isInteractionActive = isActive;
    }
}
