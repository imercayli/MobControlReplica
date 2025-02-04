using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class CounterBoxObstacle : ObstacleBase
{
    [SerializeField] private float boxHealthAmount;
    [SerializeField] private TextMeshPro boxHealthAmountText;
    private Vector3 healthAmountTextOrjinScale;
    private Vector3 orjinScale;

    protected SoundService soundService;
   
    protected virtual void Start()
    {
        SetHealthText(false);
        healthAmountTextOrjinScale = boxHealthAmountText.transform.localScale;
        orjinScale = transform.lossyScale;
        soundService = ServiceSystem.GetService<SoundService>();
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
        Shake();
        soundService.PlaySound("JellyBounce");
        
        if(boxHealthAmount<=0)
            DestoryBox();
    }

    private void Shake()
    {
        transform.DOKill();
        transform.localScale = orjinScale;
        transform.DOShakeScale(0.1f, 1);
    } 

    protected virtual void DestoryBox()
    {
        //soundService.PlaySound("Crash");
        Destroy(gameObject);
    }
}
