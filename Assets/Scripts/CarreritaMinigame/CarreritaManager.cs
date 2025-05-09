using System.Collections;
using UnityEngine;

public class CarreritaManager : MonoBehaviour
{

    public Transform topSpawner;
    public Transform midSpawner;
    public Transform botSpawner;
    
    public float obstacleSpeed = 3f;

    public float spawnInterval = 2f;
    public GameObject obstaclePrefab;
    
    private Transform[] spawnPoints;
    
    public float doubleSpawnChance = 0.0f; // 30% chance to spawn two obstacles

    public float managerTimer;
    void Start()
    {
        // Initialize the spawn points array
        spawnPoints = new Transform[] { topSpawner, midSpawner, botSpawner };

        // Start the spawning coroutine
        StartCoroutine(SpawnObstaclesPeriodically());
    }

    // Update is called once per frame
    void Update()
    {
        managerTimer += Time.deltaTime;

        if (managerTimer <= 5f)
        {
            obstacleSpeed = 3.0f;
            doubleSpawnChance = 0.0f;
            spawnInterval = 2f;
        }
        else if (managerTimer > 5f && managerTimer <= 10f)
        {
            obstacleSpeed = 4.0f;
            doubleSpawnChance = 2.5f;
            spawnInterval = 1.75f;
        }
        else if (managerTimer > 10f && managerTimer <= 15f)
        {
            obstacleSpeed = 5.0f;
            doubleSpawnChance = 5.0f;
            spawnInterval = 1.5f;
        }
        else if(managerTimer > 15f)
        {
            obstacleSpeed = 6.0f;
            doubleSpawnChance = 7.5f;
            spawnInterval = 1f;
        }
    }
    
    IEnumerator SpawnObstaclesPeriodically()
    {
        while (true)
        {
            // Wait for the specified interval before spawning the next obstacle
            yield return new WaitForSeconds(spawnInterval);

            // Initialize a list of spawn indexes to track chosen spawn points
            bool[] usedSpawnPoints = new bool[spawnPoints.Length];

            // Choose one spawn point and spawn the obstacle
            int firstSpawnIndex = Random.Range(0, spawnPoints.Length);
            SpawnObstacleAt(firstSpawnIndex);
            usedSpawnPoints[firstSpawnIndex] = true;

            // Randomly decide if a second obstacle should spawn
            if (Random.value < doubleSpawnChance) // Random.value gives a float between 0 and 1
            {
                // Choose another spawn point that hasn't been used
                int secondSpawnIndex;
                do
                {
                    secondSpawnIndex = Random.Range(0, spawnPoints.Length);
                } while (usedSpawnPoints[secondSpawnIndex]);

                // Spawn the second obstacle
                SpawnObstacleAt(secondSpawnIndex);
            }
        }
    }

    private void SpawnObstacleAt(int index)
    {
        Transform chosenSpawnPoint = spawnPoints[index];
        Instantiate(obstaclePrefab, chosenSpawnPoint.position, Quaternion.identity);
        // Optionally, set the obstacle's speed if needed
    }
    
    
}
