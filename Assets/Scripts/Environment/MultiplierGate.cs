using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class MultiplierGate : MonoBehaviour,IPlayerInteractable
{
    [SerializeField] private CalculationType calculationType;
    [SerializeField] private int calculationAmount;
    [SerializeField] private TextMeshPro amountText;
    [SerializeField] private bool isMoving;
    [ShowIf("isMoving")]
    [SerializeField] private float xPositionOffset,movementSpeed;
    
    
    void Start()
    {
        SetAmountText();
        Move();
    }

    private void SetAmountText()
    {
        string operationSymbol = calculationType switch
        {
            CalculationType.Multiplier => "x",
            CalculationType.Divider => "/",//implement later
            CalculationType.Adder => "+",
            CalculationType.Subtractor => "-",
            _ => "x"
        };

        amountText.text = $"{operationSymbol}{calculationAmount}";
    }

    private void Move()
    {
        if(!isMoving) return;

        Vector3 targetPos = transform.position + transform.right * xPositionOffset;
        transform.DOMove(targetPos, movementSpeed)
            .SetSpeedBased(true)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }
    
    public void Interact(Player player)
    {
        if(player.GetComponent<CharacterInteraction>().createdMultiplierGate == this) return;

        for (int i = 0; i < calculationAmount-1; i++)
        {
            Player newPlayer = FindObjectOfType<PlayerFactory>()
                .CreateInstance(player.transform.position, player.transform.rotation);
            newPlayer.GetComponent<CharacterMovement>().
                SetTraget(FindObjectOfType<EnemyFortress>().transform.position);
            newPlayer.GetComponent<CharacterInteraction>().createdMultiplierGate = this;
            FindObjectOfType<CharacterSpawnSmokeParticleFactory>()
                .CreateInstance(player.transform.position, player.transform.rotation);

        }
    }
}
