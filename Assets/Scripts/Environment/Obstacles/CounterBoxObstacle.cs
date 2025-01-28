using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CounterBoxObstacle : ObstacleBase
{
    [SerializeField] private int boxHealthAmount;
    [SerializeField] private TextMeshPro boxHealthAmountText;
   
    protected virtual void Start()
    {
        SetHealthText();
    }

    private void SetHealthText()
    {
        boxHealthAmountText.text = boxHealthAmount.ToString();
    }

    public override void Interact(Player player)
    {
        base.Interact(player);
        TakeDamage();
    }

    private void TakeDamage()
    {
        boxHealthAmount--;
        SetHealthText();
        
        if(boxHealthAmount<=0)
            Die();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
