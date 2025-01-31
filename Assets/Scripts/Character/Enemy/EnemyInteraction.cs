using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteraction : CharacterInteraction,IPlayerInteractable
{
    public void InteractPlayer(Player player)
    {
        characterBase.CharacterHealth.TakeDamage(player.CharacterAttack.DamageAmount);
        player.CharacterHealth.TakeDamage(characterBase.CharacterAttack.DamageAmount);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if(!isInteractionActive) return;
        
        base.OnTriggerEnter(other);
        
        if (other.TryGetComponent(out IEnemyInteractable enemyInteractable))
        {
            enemyInteractable.InteractEnemy(characterBase as Enemy);
        }
    }
}
