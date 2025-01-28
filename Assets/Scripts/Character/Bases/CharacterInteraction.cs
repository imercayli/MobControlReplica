using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterInteraction : MonoBehaviour
{
    protected bool isInteractionActive;
    [SerializeField] private Collider characterCollider;
    [FormerlySerializedAs("createdBoostGate")] public MultiplierGate createdMultiplierGate;
    
    private void OnEnable()
    {
        SetInteractionActivation(true);
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if(!isInteractionActive) return;
        
    }
    public void SetInteractionActivation(bool isActive)
    {
        isInteractionActive = isActive;
        characterCollider.enabled = isActive;
    }
}
