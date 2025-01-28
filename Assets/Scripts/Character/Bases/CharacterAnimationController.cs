using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Animation Events will be here
/// </summary>
public class CharacterAnimationController : MonoBehaviour
{
   private CharacterBase characterBase;

   private void Awake()
   {
      characterBase = GetComponentInParent<CharacterBase>();
   }
}
