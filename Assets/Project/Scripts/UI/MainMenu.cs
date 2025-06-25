using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private EnemySpawner spawner; // Reference to "EnemySpawner" script

    public void FullReset()
    {
        DestroyAllTowers();
        ResetAllPlots();
        ResetLevelStats();
        EndCurrentRound();
        ResetTimerUi();

        Debug.Log("A teljes csatatér megtisztítva, uram!");
    }  // Resets the "GameScene"
    public void Quit()
    {
        Application.Quit();
    } // Exits the program
    public GameObject[] levels;
    public void Exit()
    {
        foreach (GameObject level in levels)
            level.SetActive(false);

        FullReset();
    } // Exits "GameScene"
    public void DestroyAllTowers()
    {
        GameObject[] castles = GameObject.FindGameObjectsWithTag("Castle");
        GameObject[] mines = GameObject.FindGameObjectsWithTag("stonemine");

        foreach (var tower in castles)
            Destroy(tower);
        foreach (var tower in mines)
            Destroy(tower);
    } // Destroys the all the towers in "GameScene"
    public void ResetLevelStats()
    {
        LevelManager levelManager;
        levelManager = FindObjectOfType<LevelManager>();
        EnemySpawner enemySpawner;
        enemySpawner = FindObjectOfType<EnemySpawner>();
        levelManager.health = 100;
        levelManager.kills = 0;
        levelManager.currency = 100;
        enemySpawner.currentWave = 1;
    } // Reset all lvlstats in "GameScene"
    public void ResetAllPlots()
    {
        foreach (Plot plot in MapBuilder.plotsInLevel)
        {
            if (plot != null)
            {
                plot.gameObject.SetActive(true);
                plot.ResetPlot();
            }
        }
    } // Reset all plots in "GameScene"
    public void EndCurrentRound()
    {
        if (spawner != null)
        {
            spawner.StopAllCoroutines();
            spawner.isSpawning = false;
            spawner.isWaveActive = false;
        }

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        Debug.Log("🏁 A jelenlegi kör befejeződött, minden ellenség elpusztult, uram!");
    } // Ends ongoing Round in "Gamescene"
    public void ResetTimerUi()
    {
        TimerManager.Instance.isGameRunning = false;
        TimerManager.Instance.isFastForward = false;

        TimerUI timerUI = FindObjectOfType<TimerUI>();
        if (timerUI != null)
        {
            timerUI.UpdateStartStopSprite();
            timerUI.UpdateSpeedSprite();
        }
    } // Resets the timerbar in "GameScene"
}
