using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFortress : MonoBehaviour
{
    [SerializeField] private int minSpawnAmount, maxSpawnAmount;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float spawnRate;
    private float spawnTimer;
    
    // Start is called before the first frame update
    void Start()
    {
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
                .CreateInstance(EnemyType.Normal,spawnPoint.position, spawnPoint.rotation);
            enemy.transform.position = spawnPoint.position;
            enemy.transform.rotation = spawnPoint.rotation;
            enemy.GetComponent<CharacterMovement>().SetTraget(FindObjectOfType<CanonMovement>().transform.position);
        }

        spawnTimer = Time.time + spawnRate;
    }
}
