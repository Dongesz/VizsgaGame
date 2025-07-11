// @desc: Spawns enemy prefabs based on Enemy data and initializes their individual behaviours.
// @lastWritten: 2025-07-09
// @upToDate: false
using CastL.Data;
using CastL.Managers;
using CastL.Prefab;
using CastL.System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CastL.System
{
    public class EnemySpawner : MonoBehaviour
    {
        public static EnemySpawner Instance;

        [SerializeField] private List<Enemy> enemyTypes;

        public bool isSpawning = false;
        public float timeSinceLastSpawn;
        public int enemiesLeftToSpawn;

        private void Awake()
        {
                Instance = this;
        }

        private void Update()
        {
            timeSinceLastSpawn += Time.deltaTime;
        }

        public void SpawnEnemy()
        {
            if (LevelDataHolder.Instance.StartPoint == null)
            {
                Debug.LogError("StartPoint nem elérhető, nem lehet spawnolni!");
                return;
            }

            Enemy chosen = enemyTypes[Random.Range(0, enemyTypes.Count)];
            GameObject go = Instantiate(
                chosen.prefab,
                LevelDataHolder.Instance.StartPoint.position,
                Quaternion.identity
                );

            var behaviour = go.GetComponent<EnemyBehaviour>();
            if (behaviour != null)
            {
                behaviour.Initialize(chosen);
            }
            else
            {
                Debug.Log("A prefabon nincs EnemyBehaviour script!");
            }

        }
        
    }
}
