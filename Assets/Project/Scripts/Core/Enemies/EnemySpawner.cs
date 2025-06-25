using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Attributes")]
    [SerializeField] private int BaseEnemies = 8;
    [SerializeField] private float enemiesPerSec = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    public int currentWave = 0;
    private float timeSinceLastSpawn;
    public int enemiesAlive;
    private int enemiesLeftToSpawn;
    public bool isSpawning = false;
    public bool isWaveActive = false;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    private void Update()
    {
        if (!isSpawning) return;
        WinWave();

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesPerSec) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (isSpawning && enemiesLeftToSpawn <= 0 && enemiesAlive <= 0)
        {
            EndWave();
        }
    }

    private void SpawnEnemy()
    {
        if (LevelManager.main == null || LevelManager.main.StartPoint == null)
        {
            Debug.LogError("StartPoint nem elérhető, nem lehet spawnolni!");
            return;
        }

        GameObject prefabToSpawn = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        Instantiate(prefabToSpawn, LevelManager.main.StartPoint.position, Quaternion.identity);
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }

    public void BeginNextWave()
    {
        if (isWaveActive || isSpawning) return;
        StartCoroutine(StartWave());
    }

    private IEnumerator StartWave()
    {
        yield return new WaitForSeconds(0.1f); 
        isWaveActive = true;
        yield return new WaitForSeconds(timeBetweenWaves);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    private void EndWave()
    {
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        isWaveActive = false;

        TimerManager.Instance.isGameRunning = false;

        TimerUI ui = FindObjectOfType<TimerUI>();
        if (ui != null)
            ui.UpdateStartStopSprite();
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(BaseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    public void StopAllWaves()
    {
        StopAllCoroutines();
        isSpawning = false;
        isWaveActive = false;
        timeSinceLastSpawn = 0f;
        enemiesLeftToSpawn = 0;
        enemiesAlive = 0;

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        Debug.Log("Hullámok leállítva, ellenségek eltakarítva, uram!");
    }

    public void WinWave()
    {
        if (currentWave > 1)
        {
            Debug.Log("You won!");
        }
    }
}