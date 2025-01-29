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
    
    
    void Start()
    {
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

        amountText.transform.DOKill();
        amountText.transform.localScale = amountTextOrjinScale;
        amountText.transform.DOPunchScale(Vector3.one*0.1f, .2f);

        for (int i = 0; i < calculationAmount-1; i++)
        {
            Player newPlayer = ServiceSystem.GetService<PlayerFactory>()
                .CreateInstance(player.transform.position, player.transform.rotation);
            newPlayer.CharacterMovement.
                SetTraget(EnvironmentManager.Instance.EnemyFortress.transform.position,false);
            newPlayer.CharacterInteraction.SetMultiplierGate(this);
            ServiceSystem.GetService<CharacterSpawnSmokeParticleFactory>()
                .CreateInstance(player.transform.position, player.transform.rotation);

            //TODO
            StartCoroutine(Routine());
            IEnumerator Routine()
            {
                // player.GetComponent<NavMeshAgent>().obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
                // var nav = newPlayer.GetComponent<NavMeshAgent>();
                // nav.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
                //
                // yield return new WaitForSeconds(2f);
                // nav.obstacleAvoidanceType = ObstacleAvoidanceType.MedQualityObstacleAvoidance;
                // player.GetComponent<NavMeshAgent>().obstacleAvoidanceType = ObstacleAvoidanceType.MedQualityObstacleAvoidance;

                player.GetComponent<NavMeshAgent>().enabled = false;
                newPlayer.GetComponent<NavMeshAgent>().enabled = false;
                player.transform.DOMove(player.transform.position + player.transform.forward * 1f + transform.right * -.2f,
                    0.1f);
                newPlayer.transform.DOMove(newPlayer.transform.position + newPlayer.transform.forward * 1f + transform.right * .2f,
                    0.1f);
                yield return new WaitForSeconds(0.1f);
                player.GetComponent<NavMeshAgent>().enabled = true;
                newPlayer.GetComponent<NavMeshAgent>().enabled = true;
                player.CharacterMovement.
                    SetTraget(FindObjectOfType<EnemyFortress>().transform.position);
                newPlayer.CharacterMovement.
                    SetTraget(FindObjectOfType<EnemyFortress>().transform.position);
            }
        }
    }
}
