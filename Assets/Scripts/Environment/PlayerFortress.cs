using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFortress : MonoBehaviour,IEnemyInteractable
{
    public void Interact(Enemy enemy)
    {
        GameManager.Instance.GameOver(false);
    }
}
