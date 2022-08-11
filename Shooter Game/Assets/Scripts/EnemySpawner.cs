using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    // variables
    public int enemyCount;
    public int enemyAmount;
    public bool isGameRunning;
    // end of variables

    // components
    public PlayerController playerControllerScript;
    // end of components

    // game objects
    public GameObject[] enemyPrefabs;
    // end of game objects


    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        isGameRunning = playerControllerScript.isGameRunning;

        SpawnEnemyWave(enemyAmount);

    }

    // Update is called once per frame
    void Update()
    {
        isGameRunning = playerControllerScript.isGameRunning;

        if (isGameRunning)
        {
            enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length; // Count of enemy in game scene


            // Spawn one more enemy then previous wave
            if (enemyCount <= 0)
            {
                enemyAmount++;
                SpawnEnemyWave(enemyAmount);

            }
        }

    }

    private void SpawnEnemyWave(int enemyToSpawn)
    {

        for (int i = 0; i < enemyToSpawn; i++)
        {
            int enemyIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[enemyIndex], GenerateRandomPosition(), enemyPrefabs[enemyIndex].transform.rotation);
        }

    }

    private Vector3 GenerateRandomPosition()
    {

        Vector3 centerOfSpawner = transform.position;
        float minBoundX = centerOfSpawner.x - 5;
        float maxBoundX = centerOfSpawner.x + 5;
        float minBoundZ = centerOfSpawner.z - 5;
        float maxBoundZ = centerOfSpawner.z + 5;

        float randomXPos = Random.Range(minBoundX, maxBoundX);
        float randomZPos = Random.Range(minBoundZ, maxBoundZ);

        Vector3 randPos = new Vector3(randomXPos, transform.position.y, randomZPos);

        return randPos;

    }
}
