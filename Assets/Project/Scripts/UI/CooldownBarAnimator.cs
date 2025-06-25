using System.Collections;
using UnityEngine;

public class CooldownBarAnimator : MonoBehaviour
{
    public SpriteRenderer cooldownRenderer; // A SpriteRenderer komponens, amely a cooldown sprite-ot jeleníti meg
    public Sprite[] cooldownSprites; // A 7 sprite tárolása
    public Sprite cooldownSpritesstop; 
    public float cooldownDuration = 2.0f; // Teljes cooldown idõ
    private float timePerFrame;
    private int currentFrame;
    private float timer;

    private TimerManager TimerScript;
    private EnemySpawner EnemySpawnerScript;


    void Start()
    {
        TimerScript = FindObjectOfType<TimerManager>();
        EnemySpawnerScript = FindObjectOfType<EnemySpawner>();
        timePerFrame = cooldownDuration / cooldownSprites.Length;
        currentFrame = 0;
        timer = 0f;
    }
    void Update()
    {
        StartMining();
    }


    public void StartMining()
    {
        if(EnemySpawnerScript.isWaveActive)
        {
            // Idõ frissítése
            timer += Time.deltaTime;

            if (timer >= timePerFrame)
            {
                timer -= timePerFrame;
                currentFrame = (currentFrame + 1) % cooldownSprites.Length;
                cooldownRenderer.sprite = cooldownSprites[currentFrame];

                // Ha elérte az animáció végét
                if (currentFrame == 0)
                {
                    // Az animáció befejezõdött, hívja meg a LevelManager metódust
                    LevelManager.main.IncreaseCurrency(50);
                }
            }

            // A feltétel ellenõrzése a ciklus végén
        }
    }


    public void StopMining()
    {
    }



}
 