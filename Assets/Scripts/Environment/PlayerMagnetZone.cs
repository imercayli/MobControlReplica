using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMagnetZone : MonoBehaviour,IPlayerInteractable
{
    [SerializeField] private EnemyFortress enemyFortress;
    
    public void Interact(Player player)
    {
        player.GetComponent<NavMeshAgent>().SetDestination(enemyFortress.transform.position);
    }
}
