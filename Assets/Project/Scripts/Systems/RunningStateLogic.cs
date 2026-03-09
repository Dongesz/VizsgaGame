// @desc: Handles enemy spawning while the game is in the Running state, using a random spawn interval between configurable bounds.
// @lastWritten: 2025-07-10
// @upToDate: false
using CastL.Data;
using CastL.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningStateLogic : MonoBehaviour
{
    [Header("Spawn idő intervallum (másodperc)")]
    public float minSpawnRate = 0.5f;
    public float maxSpawnRate = 2.0f;

    private float currentSpawnRate;
    private float spawnTimer;

    private void Awake()
    {
        // Biztonság kedvéért: ne legyen fordított tartomány
        if (maxSpawnRate < minSpawnRate)
        {
            float tmp = minSpawnRate;
            minSpawnRate = maxSpawnRate;
            maxSpawnRate = tmp;
        }
    }

    private void SetNextSpawnRate()
    {
        currentSpawnRate = Random.Range(minSpawnRate, maxSpawnRate);
    }

    public void OnEnable()
    {
        spawnTimer = 0f;
        SetNextSpawnRate();
    }

    public void SpawningTick()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer >= currentSpawnRate)
        {
            spawnTimer = 0f;
            EnemySpawner.Instance.SpawnEnemy();
            SetNextSpawnRate();
        }
    }
}
