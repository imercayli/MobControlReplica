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
         Player newPlayer = ServiceSystem.GetService<PlayerFactory>()
            .CreateInstance(transform.position, transform.rotation);
         newPlayer.CharacterMovement.
            SetTraget(EnvironmentManager.Instance.EnemyFortress.transform.position);
      }
   }
}
