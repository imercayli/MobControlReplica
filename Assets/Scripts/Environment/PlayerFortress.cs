using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFortress : MonoBehaviour,IEnemyInteractable
{
    public void Interact(Enemy enemy)
    {
        ServiceSystem.GetService<GameService>().GameOver(false);
    }
}
