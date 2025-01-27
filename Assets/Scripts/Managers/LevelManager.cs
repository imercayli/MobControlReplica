using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Extentions;
using Unity.AI.Navigation;
using UnityEditor;

public class LevelManager : MonoSingleton<LevelManager>
{
    public GameObject CurrentLevel { get; private set; }

    public List<GameObject> Levels;
    public NavMeshSurface NavMeshSurface;

    protected override void Awake()
    {
        base.Awake();
        
        // if(Levels == null)
        // {
        //     Levels = new List<GameObject>(Resources.LoadAll<GameObject>("Levels"));
        // }

    }

    private void Start()
    {
       // GameManager.Instance.OnGameStart += SpawnLevel;
     //  NavMeshSurface navMeshSurface = levelInstance.GetComponent<NavMeshSurface>();
       // if (navMeshSurface != null)
       // {
       //     // Bake the NavMesh dynamically
       // }
       
       NavMeshSurface.BuildNavMesh();
    }

    private void SpawnLevel()
    {
       
       // CurrentLevel = Instantiate(Levels[SaveData.GameLevel % Levels.Count]);

    }

}
