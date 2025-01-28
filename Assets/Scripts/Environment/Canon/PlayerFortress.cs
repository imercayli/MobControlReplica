using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFortress : MonoBehaviour,IEnemyInteractable
{
    public void InteractEnemy(Enemy enemy)
    {
        ServiceSystem.GetService<GameService>().GameOver(false);
    }
}
