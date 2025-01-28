using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleBase : MonoBehaviour,IPlayerInteractable
{
    [SerializeField] private int damageAmount;
    
    public virtual void InteractPlayer(Player player)
    {
        player.CharacterHealth.TakeDamage(damageAmount);
    }
}
