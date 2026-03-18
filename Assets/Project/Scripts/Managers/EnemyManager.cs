// @desc: Manages global enemy tracking, registration, and destruction across all enemy instances.
// @lastWritten: 2025-07-05
// @upToDate: true
using System.Collections;
using System.Collections.Generic;
using CastL.Prefabs;
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
            // Elsődlegesen komponens alapján törlünk, így nem kell a Tag-re támaszkodni.
            var behaviours = Object.FindObjectsByType<EnemyBehaviour>(FindObjectsSortMode.None);
            foreach (var b in behaviours)
            {
                if (b != null) Destroy(b.gameObject);
            }

            // Ha esetleg maradt olyan enemy, amin nincs EnemyBehaviour, a régi Tag-es törlés is lefut.
            var enemiesByTag = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemiesByTag)
            {
                if (enemy != null) Destroy(enemy);
            }

            activeEnemies = 0;
        }
    }
}

