using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrapContainerObstacle : CounterBoxObstacle
{
   [SerializeField] private int trapPlayerCount;

   protected override void DestoryBox()
   {
      SpawnPlayers();
      base.DestoryBox();
   }

   private void SpawnPlayers()
   {
      for (int i = 0; i < trapPlayerCount; i++)
      {
         float radius = 4f;
         Vector3 targetPosition = transform.position + Random.insideUnitSphere * radius;
         targetPosition.y = transform.position.y;
         Player newPlayer = ServiceSystem.GetService<PlayerFactory>()
            .CreateInstance(targetPosition, transform.rotation);
         newPlayer.CharacterMovement.
            SetTargetForward(EnvironmentManager.Instance.EnemyFortress.transform.position,true);
      }
   }
}
