using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterInteraction : MonoBehaviour
{
    protected CharacterBase characterBase;
    protected bool isInteractionActive;
    [SerializeField] private Collider characterCollider;
    public MultiplierGate CreatedMultiplierGate { get; private set; }
    
    public void Initialize(CharacterBase characterBase)
    {
        this.characterBase = characterBase;
        this.characterBase.OnDie += () => { SetInteractionActivation(false); };
        SetInteractionActivation(true);
        CreatedMultiplierGate = null;
    }
    
    protected virtual void OnTriggerEnter(Collider other)
    {
        if(!isInteractionActive) return;
        
    }

    public void SetMultiplierGate(MultiplierGate multiplierGate)
    {
        CreatedMultiplierGate = multiplierGate;
    }
    public void SetInteractionActivation(bool isActive)
    {
        isInteractionActive = isActive;
        characterCollider.enabled = isActive;
    }

    private void OnDisable()
    {
        if(characterBase)
          characterBase.OnDie -= () => { SetInteractionActivation(false); };
    }
}
