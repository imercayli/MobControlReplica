using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteraction : CharacterInteraction,IPlayerInteractable
{
    public void Interact(Player player)
    {
        GetComponent<CharacterHealth>().TakeDamage(1);
        player.GetComponent<CharacterHealth>().TakeDamage(1);
    }
}
