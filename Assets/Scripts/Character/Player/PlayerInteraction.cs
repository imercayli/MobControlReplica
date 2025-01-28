using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : CharacterInteraction
{
   protected override void OnTriggerEnter(Collider other)
   {
      if(!isInteractionActive) return;
      
      base.OnTriggerEnter(other);

      if (other.TryGetComponent(out IPlayerInteractable playerInteractable))
      {
         playerInteractable.InteractPlayer(characterBase as Player);
      }
   }
}
