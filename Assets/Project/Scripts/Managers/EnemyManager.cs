// @desc: Manages global enemy tracking, registration, and destruction across all enemy instances.
// @lastWritten: 2025-07-05
// @upToDate: true
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastL.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        public static EnemyManager Instance;

        public int activeEnemies = 0;

        private void Awake()
        {
            Instance = this;
        }

        public void RegisterEnemy()
        {
            activeEnemies++;
        }

        public void UnRegisterEnemy()
        {
            activeEnemies--;
            if (activeEnemies <= 0)
            {
                Debug.Log("Minden Enemy halott!");
            }
        }
        public void DestroyAllEnemies()
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                Destroy(enemy);
            }
        }
    }
}

