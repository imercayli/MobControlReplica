using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GiantHealth : CharacterHealth
{
   private Vector3 orjinScale;

   private void Start()
   {
      orjinScale = transform.localScale;
   }

   public override void TakeDamage(float damageAmount)
   {
      base.TakeDamage(damageAmount);

      if(isDeath || isImmune) return;
      
      transform.DOKill();
      Vector3 scale = Vector3.Lerp(Vector3.one, orjinScale, health / maxHealth);
      transform.DOScale(scale, 0.2f);

      StartCoroutine(Routine());
      IEnumerator Routine()
      {
         SetImmune(true);
         yield return new WaitForSeconds(0.5f);
         SetImmune(false);
      }
   }
}
