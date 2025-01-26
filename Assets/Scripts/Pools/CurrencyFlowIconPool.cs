using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class CurrencyFlowIconPool : MonoSingleton<CurrencyFlowIconPool>
{
   [SerializeField] private CharacterMovement player;

   public CharacterMovement GetPlayer()
   {
      return LeanPool.Spawn(player);
   }
}
