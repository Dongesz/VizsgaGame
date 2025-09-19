// @desc: 
// @lastWritten: 2025-07-10
// @upToDate: false
using CastL.Data;
using CastL.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningStateLogic : MonoBehaviour
{
    public float currentSpawnRate = 1.0f;
    public float spawnTimer;
    public void OnEnable()
    {
        spawnTimer = 0f;
    }

    public void SpawningTick()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer >= currentSpawnRate)
        {
            spawnTimer = 0f;
            EnemySpawner.Instance.SpawnEnemy();
        }
    }
}
