using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class LevelService : BaseService<LevelService>
{
    public GameObject CurrentLevel { get; private set; }

    public List<GameObject> Levels;
    public NavMeshSurface NavMeshSurface;
    
    public override void Initialize()
    {
        base.Initialize();
        // if(Levels == null)
        // {
        //     Levels = new List<GameObject>(Resources.LoadAll<GameObject>("Levels"));
        // }
        // GameManager.Instance.OnGameStart += SpawnLevel;
        //  NavMeshSurface navMeshSurface = levelInstance.GetComponent<NavMeshSurface>();
        // if (navMeshSurface != null)
        // {
        //     // Bake the NavMesh dynamically
        // }
       
        NavMeshSurface.BuildNavMesh();
    }
}
