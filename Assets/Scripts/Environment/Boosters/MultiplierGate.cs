using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class MultiplierGate : MonoBehaviour,IPlayerInteractable
{
    [SerializeField] private CalculationType calculationType;
    [SerializeField] private int calculationAmount;
    [SerializeField] private TextMeshPro amountText;
    private Vector3 amountTextOrjinScale;
    [SerializeField] private bool isMoving;
    [ShowIf("isMoving")]
    [SerializeField] private float xPositionOffset,movementSpeed;

    private GameService gameService;
    private SoundService soundService;
    
    void Start()
    {
        gameService = ServiceSystem.GetService<GameService>();
        soundService = ServiceSystem.GetService<SoundService>();
        SetAmountText();
        Move();
    }

    /// <summary>
    /// Other types might be implement later
    /// </summary>
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
        amountTextOrjinScale = amountText.transform.localScale;
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
    
    public void InteractPlayer(Player player)
    {
        if(player.CharacterInteraction.CreatedMultiplierGate == this) return;
        
        if(gameService.IsGameOver) return;

        amountText.transform.DOKill();
        amountText.transform.localScale = amountTextOrjinScale;
        amountText.transform.DOPunchScale(Vector3.one*0.1f, .2f);

        for (int i = 0; i < calculationAmount-1; i++)
        {
            Player newPlayer = ServiceSystem.GetService<PlayerFactory>()
                .CreateInstance(player.transform.position, player.transform.rotation);
            newPlayer.CharacterMovement.
                SetTargetForward(EnvironmentManager.Instance.EnemyFortress.transform.position);
            newPlayer.CharacterInteraction.SetMultiplierGate(this);
            ServiceSystem.GetService<CharacterSpawnSmokeParticleFactory>()
                .CreateInstance(player.transform.position, player.transform.rotation);
        }
        
       // soundService.PlaySound("MultiplierBoost");
    }
}
