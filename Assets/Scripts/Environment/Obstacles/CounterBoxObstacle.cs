using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CounterBoxObstacle : ObstacleBase
{
    [SerializeField] private float boxHealthAmount;
    [SerializeField] private TextMeshPro boxHealthAmountText;
   
    protected virtual void Start()
    {
        SetHealthText();
    }

    private void SetHealthText()
    {
        boxHealthAmountText.text = boxHealthAmount.ToString();
    }

    public override void InteractPlayer(Player player)
    {
        base.InteractPlayer(player);
        TakeDamage(player.CharacterAttack.DamageAmount);
    }

    private void TakeDamage(float damageAmount)
    {
        boxHealthAmount -= damageAmount;
        SetHealthText();
        
        if(boxHealthAmount<=0)
            DestoryBox();
    }

    protected virtual void DestoryBox()
    {
        Destroy(gameObject);
    }
}
