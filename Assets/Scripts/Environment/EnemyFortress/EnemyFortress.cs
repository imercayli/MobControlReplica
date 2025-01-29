using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyFortress : CounterBoxObstacle
{
    private GameService gameService;
    private bool isSpawningActive;

    [SerializeField] private Transform spawnPoint;
    [Header("Spawn Rate")]
    [SerializeField] private AnimationCurve spawnRateAnimationCurve;
    [SerializeField] private float minSpawnRate, maxSpawnRate;
    [SerializeField] private int maxSpawnRateReachValue;

    [Header("Spawn Amount")]
    [SerializeField] private int minSpawnAmount;
    [SerializeField] private int maxSpawnAmount;
    
    private float spawnTimer;
    private int totalEnemyCount,totalSpawnGroupCount;

    [BoxGroup("Giant Spawn Info")] 
    [SerializeField] private int giantFirstApperanceValue, giantSpawnRate;
    
    protected override void Start()
    {
        base.Start();
        SetAnimationCurve();
        SetSpawningActivation(true);
        gameService = ServiceSystem.GetService<GameService>();
        gameService.OnGameOver += (isSuccess) => { SetSpawningActivation(false); };
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemies();
    }

    protected void SetAnimationCurve()
    {
        Keyframe[] keys = new[]
        {
            new Keyframe(0f, minSpawnRate),
            new Keyframe(1f, maxSpawnRate)
        };

        spawnRateAnimationCurve.keys = keys;
    }

    private void SpawnEnemies()
    {
        if(!isSpawningActive || spawnTimer>Time.time) return;

        int totalSpawnCount = Random.Range(minSpawnAmount, maxSpawnAmount + 1);
        
        for (int i = 0; i < totalSpawnCount; i++)
        {
            bool isGiant = totalEnemyCount >= giantFirstApperanceValue && totalEnemyCount % giantSpawnRate == 0;
            Enemy enemy =  ServiceSystem.GetService<EnemiesFactory>()
                .CreateInstance(isGiant ? EnemyType.Giant : EnemyType.Normal,spawnPoint.transform.position, spawnPoint.transform.rotation);
            enemy.CharacterMovement.SetTargetForward(EnvironmentManager.Instance.Canon.transform.position);
            totalEnemyCount++;
        }
        
        spawnTimer = Time.time + GetSpawnRate();
        totalSpawnGroupCount++;
    }

    private float GetSpawnRate()
    {
        float t = Mathf.InverseLerp(0, maxSpawnRateReachValue, totalSpawnGroupCount);
        return spawnRateAnimationCurve.Evaluate(t);
    }

    protected override void DestoryBox()
    {
        ServiceSystem.GetService<GameService>().GameOver(true);
        base.DestoryBox();
    }
    
    public void SetSpawningActivation(bool isActive)
    {
        isSpawningActive = isActive;
    }

    private void OnDestroy()
    {
        gameService.OnGameOver -= (isSuccess) => { SetSpawningActivation(false); };
    }
}
