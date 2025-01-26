using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using TMPro;
using UnityEngine;

public class BoostGate : MonoBehaviour,IPlayerInteractable
{
    [SerializeField] private CalculationType calculationType;
    [SerializeField] private int calculationAmount;
    [SerializeField] private TextMeshPro amountText;
    [SerializeField] private bool isMoving;
    [SerializeField] private float movementOffset;

    public int Amount => calculationAmount;//todo
    
    void Start()
    {
        SetAmountText();
    }

    private void SetAmountText()
    {
        string operationSymbol = calculationType switch
        {
            CalculationType.Multiplier => "x",
            CalculationType.Divider => "/",
            CalculationType.Adder => "+",
            CalculationType.Subtractor => "-",
            _ => "x"
        };

        amountText.text = $"{operationSymbol}{calculationAmount}";
    }

    private void Update()
    {
        MoveGate();
    }

    private void MoveGate()
    {
        if (!isMoving) return;
        
        
    }

    public void Interact(Player player)
    {
        if(player.GetComponent<CharacterInteraction>().createdBoostGate == this) return;

        for (int i = 0; i < Amount; i++)
        {
            CharacterMovement xCharacterMovement = CurrencyFlowIconPool.Instance.GetPlayer();
            xCharacterMovement.transform.position = player.transform.position;
            xCharacterMovement.transform.rotation = player.transform.rotation;
            xCharacterMovement.GetComponent<CharacterInteraction>().createdBoostGate = this;
        }
            
        LeanPool.Despawn(player);
    }
}
