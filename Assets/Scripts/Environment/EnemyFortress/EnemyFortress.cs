using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class EnemyFortress : CounterBoxObstacle
{
    private bool isSpawningActive;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int minSpawnAmount, maxSpawnAmount;
    [SerializeField] private float spawnRate;
    private float spawnTimer;

    [BoxGroup("Giant Spawn Info")] 
    [SerializeField] private int giantFirstSpawnLimit, giantSpawnRate;
    
    private int totalEnemySpawnCount;
    
    protected override void Start()
    {
        base.Start();
        spawnTimer = Time.time + spawnRate;
        SetSpawningActivation(true);
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        if(!isSpawningActive || spawnTimer>Time.time) return;

        int spawnCount = Random.Range(minSpawnAmount, maxSpawnAmount + 1);

        for (int i = 0; i < spawnCount; i++)
        {
            bool isGiant = totalEnemySpawnCount >= giantFirstSpawnLimit && totalEnemySpawnCount % giantSpawnRate == 0;
            Enemy enemy =  ServiceSystem.GetService<EnemiesFactory>()
                .CreateInstance(isGiant ? EnemyType.Giant : EnemyType.Normal,spawnPoint.transform.position, spawnPoint.transform.rotation);
            enemy.CharacterMovement.SetTraget(EnvironmentManager.Instance.Canon.transform.position);
            totalEnemySpawnCount++;
        }

      
        spawnTimer = Time.time + spawnRate;
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
}
