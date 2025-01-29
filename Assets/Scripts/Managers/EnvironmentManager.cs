using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class EnvironmentManager : MonoSingleton<EnvironmentManager>
{
    [SerializeField] private NavMeshSurface navMeshSurface;
    [SerializeField] private CanonLevelController canonLevelController;
    [SerializeField] private EnemyFortress enemyFortress;

    public CanonLevelController CanonLevelController => canonLevelController;
    public CanonMovement Canon => canonLevelController.CurrentCanon;
    public EnemyFortress EnemyFortress => enemyFortress;
    
    
    
    protected override void Awake()
    {
        base.Awake();
        navMeshSurface.BuildNavMesh();
    }
}
