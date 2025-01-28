using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class LevelService : BaseService<LevelService>
{
    public GameObject CurrentLevel { get; private set; }

    public List<GameObject> Levels;
   
    
    public override void Initialize()
    {
        base.Initialize();
        // if(Levels == null)
        // {
        //     Levels = new List<GameObject>(Resources.LoadAll<GameObject>("Levels"));
        // }
        // GameManager.Instance.OnGameStart += SpawnLevel;
    }
}
