using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currencyUI;
    [SerializeField] TextMeshProUGUI killsUI;
    [SerializeField] TextMeshProUGUI HealthUI;
    [SerializeField] TextMeshProUGUI WaveUI;
    [SerializeField] Animator anim;
    [SerializeField] private UnityEngine.UI.Image imageComponent;
    [SerializeField] private Sprite sprite1;       
    [SerializeField] private Sprite sprite2;
    private EnemySpawner EnemySpawnerScript;

    private bool isMenuOpen = true;
    private bool isSprite1Active = true;

    private void Start()
    {
        EnemySpawnerScript = FindObjectOfType<EnemySpawner>();

    }
    public void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen", isMenuOpen);
    }
    public void SwitchImg()
    {
        if (imageComponent != null)
        {
            if (isSprite1Active)
            {
                imageComponent.sprite = sprite2;   
            }
            else
            {
                imageComponent.sprite = sprite1;   
            }

            isSprite1Active = !isSprite1Active;    
        }
}

    private void OnGUI()
    {
       currencyUI.text = LevelManager.main.currency.ToString();
       killsUI.text = LevelManager.main.kills.ToString();
       HealthUI.text = LevelManager.main.health.ToString();
       WaveUI.text = EnemySpawnerScript.currentWave.ToString();
    }
}
