using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class EnvironmentManager : MonoSingleton<EnvironmentManager>
{
    [SerializeField] private NavMeshSurface navMeshSurface;
    [SerializeField] private Canon canon;
    [SerializeField] private EnemyFortress enemyFortress;

    public Canon Canon => canon;
    public EnemyFortress EnemyFortress => enemyFortress;
    
    
    
    protected override void Awake()
    {
        base.Awake();
      //  navMeshSurface.BuildNavMesh();
    }
}
