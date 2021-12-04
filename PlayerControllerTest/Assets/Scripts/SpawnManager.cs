using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemies;
    public GameObject powerUp;
    private float zEnemySpawn = 12.0f;
    private float xSpawnBound = 16.0f;
    private float zPowerUpRange = 5.0f;
    private float yRange = 0.75f;
    private float enemySpawnTime = 1.0f;
    private float powerUpSpawnTime = 5.0f;
    private float startDelay = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemy();
        SpawnPowerUp();
        InvokeRepeating("SpawnEnemy", startDelay, enemySpawnTime);
        InvokeRepeating("SpawnPowerUp", startDelay, powerUpSpawnTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SpawnEnemy()
    {
        float randomX = Random.Range(-xSpawnBound, xSpawnBound);
        int randomIndex = Random.Range(0, enemies.Length);
        Vector3 spawnPos = new Vector3(randomX, yRange, zEnemySpawn);
        Instantiate(enemies[randomIndex],spawnPos,enemies[randomIndex].transform.rotation);
    }
    void SpawnPowerUp()
    {
        float randomX = Random.Range(-xSpawnBound, xSpawnBound);
        float randomZ = Random.Range(-zPowerUpRange, zPowerUpRange);
        Vector3 randomPos = new Vector3(randomX,yRange,randomZ);
        Instantiate(powerUp,randomPos,powerUp.transform.rotation);
    }
}
