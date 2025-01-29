using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CounterBoxObstacle : ObstacleBase
{
    [SerializeField] private float boxHealthAmount;
    [SerializeField] private TextMeshPro boxHealthAmountText;
    protected Vector3 healthAmountTextOrjinScale;
   
    protected virtual void Start()
    {
        SetHealthText(false);
        healthAmountTextOrjinScale = boxHealthAmountText.transform.localScale;
    }

    private void SetHealthText(bool withAnim =true)
    {
        boxHealthAmountText.text = boxHealthAmount.ToString();

        if (withAnim)
        {
            boxHealthAmountText.transform.DOKill();
            boxHealthAmountText.transform.localScale = healthAmountTextOrjinScale;
            boxHealthAmountText.transform.DOPunchScale(Vector3.one*0.1f, .2f);
        }
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
