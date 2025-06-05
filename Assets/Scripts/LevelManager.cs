using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;
    public static EnemySpawner enemySpawner;
    public static MapBuilder mapBuilder;
    public static MainMenu mainMenu;
    public static DataBaseManager dataBaseManager;

    public Transform[] path;
    public Transform StartPoint;
    public Transform EndPoint;
    public GameObject LostScreen;
    public GameObject WinScreen;
    [SerializeField] private GameObject MainMenuObj;
    public TMP_Text currentlevel;

    public AudioSource WinSfxPlayer;
    public AudioClip WinSound;
    public AudioSource LostSfxPlayer;
    public AudioClip LostSound;

    public int currency;
    public int kills;
    public int health;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        health = 100;
        mainMenu = FindObjectOfType<MainMenu>();
        dataBaseManager = FindObjectOfType<DataBaseManager>();
    }

    private void Update()
    {
        if (health <= 0)
        {
            mainMenu.FullReset();
            LostSfxPlayer.PlayOneShot(LostSound);
            LostScreen.SetActive(true);
            return;
        }

        // Győzelemfeltétel (példa: 2-es szint)
        if (currentlevel.text == "2")
        {
            // ⬅︎ 1) SCORE mentése még reset előtt
            dataBaseManager.UpdateDatabase(kills);

            // ⬅︎ 2) most resetelhetsz
            mainMenu.FullReset();
            WinSfxPlayer.PlayOneShot(WinSound);
            WinScreen.SetActive(true);
        }
    }


    public void IncreaseCurrency(int amount) => currency += amount;
    public void IncreaseKills(int amount) => kills += amount;
    public void DecreaseHp(int amount) => health -= amount;

    public bool SpendCurrency(int amount)
    {
        if (amount > currency)
        {
            Debug.Log("Nincs elég pénzed.");
            return false;
        }

        currency -= amount;
        return true;
    }
}
