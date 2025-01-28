using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyFortress : CounterBoxObstacle//TODO
{
    [SerializeField] private int minSpawnAmount, maxSpawnAmount;
    [SerializeField] private float spawnRate;
    private float spawnTimer;
    
    protected override void Start()
    {
        base.Start();
        spawnTimer = Time.time + spawnRate;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        if(spawnTimer>Time.time) return;

        int spawnCount = Random.Range(minSpawnAmount, maxSpawnAmount + 1);

        for (int i = 0; i < spawnCount; i++)
        {
            Enemy enemy =  FindObjectOfType<EnemiesFactory>()
                .CreateInstance(EnemyType.Normal,transform.position, transform.rotation);
            enemy.GetComponent<CharacterMovement>().SetTraget(FindObjectOfType<CanonMovement>().transform.position);
        }

        spawnTimer = Time.time + spawnRate;
    }

    protected override void Die()
    {
        ServiceSystem.GetService<GameService>().GameOver(true);
        base.Die();
    }
}
