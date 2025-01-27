using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : CharacterInteraction
{
   protected override void OnTriggerEnter(Collider other)
   {
      base.OnTriggerEnter(other);

      if (other.TryGetComponent(out IPlayerInteractable playerInteractable))
      {
         playerInteractable.Interact(GetComponent<Player>());
      }
   }
}
