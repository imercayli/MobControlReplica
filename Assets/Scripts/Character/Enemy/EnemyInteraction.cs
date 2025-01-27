using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteraction : CharacterInteraction,IPlayerInteractable
{
    public void Interact(Player player)
    {
        GetComponent<CharacterHealth>().TakeDamage(1);
        player.CharacterHealth.TakeDamage(1);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        
        if (other.TryGetComponent(out IEnemyInteractable enemyInteractable))
        {
            enemyInteractable.Interact(GetComponent<Enemy>());
        }
    }
}
