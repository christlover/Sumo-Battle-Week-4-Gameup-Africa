using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{

    public GameObject enemyPrefab;
    private float spawnRange = 9;

    public int enemyCount;
    public int waveNumber = 1;

    public GameObject powerupPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //Spawn Enemy in Waves
        SpawnEnemyWaves(waveNumber);

        //Create a Powerup
        Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        //If all enemies are dead, Spawn a new wave
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0 ) {
            waveNumber++; //Increase Wave Number When All Enemies Die
            SpawnEnemyWaves(waveNumber); //Number of new enemies spawned equals the number of waves
            Instantiate(powerupPrefab,GenerateSpawnPosition(), powerupPrefab.transform.rotation); //New Powerup Each Wave
        }
    }

    //Enemy Spawn Function
    private Vector3 GenerateSpawnPosition() {
        //Choose Random spot to Spawn Enemy
        float spawnPositionX = Random.Range(-spawnRange, spawnRange);
        float spawnPositionZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPosition = new Vector3(spawnPositionX, 0, spawnPositionZ);
        return randomPosition;
    }

    //Spawn Enemy Waves
    void SpawnEnemyWaves(int enemiesToSpawn) {
        for (int i = 0; i < enemiesToSpawn; i++) {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }
}
