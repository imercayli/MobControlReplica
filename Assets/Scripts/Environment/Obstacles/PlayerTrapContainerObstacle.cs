using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerTrapContainerObstacle : CounterBoxObstacle
{
   [SerializeField] private int trapPlayerCount;
   [SerializeField] private float spawnRadius;

   protected override void DestoryBox()
   {
      SpawnPlayers();
      ServiceSystem.GetService<SoundService>().PlaySound("Escape");
      base.DestoryBox();
   }
   
   private void SpawnPlayers()
   {
      for (int i = 0; i < trapPlayerCount; i++)
      {
         float angle = i * Mathf.PI * 2f / trapPlayerCount;
         float x = transform.position.x + Mathf.Cos(angle) * spawnRadius;
         float z = transform.position.z + Mathf.Sin(angle) * spawnRadius;
         Vector3 spawnPosition = new Vector3(x, transform.position.y, z);
         Vector3 randomPos = Vector3.forward * Random.Range(-1f, 1f) + Vector3.right * Random.Range(-1f, 1f);
         spawnPosition += randomPos;
      
         Player newPlayer = ServiceSystem.GetService<PlayerFactory>()
            .CreateInstance(spawnPosition, transform.rotation);
         newPlayer.CharacterMovement.
            SetTargetForward(EnvironmentManager.Instance.EnemyFortress.transform.position,true);
      }
   }

   private void OnDrawGizmos()
   {
      Gizmos.color = Color.red;
      Gizmos.DrawSphere(transform.position, spawnRadius);
   }
}
