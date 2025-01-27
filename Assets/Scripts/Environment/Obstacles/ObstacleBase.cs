using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleBase : MonoBehaviour,IPlayerInteractable
{
    [SerializeField] private int damageAmount;
    
    public virtual void Interact(Player player)
    {
        player.GetComponent<CharacterHealth>().TakeDamage(damageAmount);
    }
}
