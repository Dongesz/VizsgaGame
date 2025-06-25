using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    [Header("UI Components")]
    [SerializeField] private Image startStopImage;
    [SerializeField] private Image speedImage;

    [Header("Sprites")]
    [SerializeField] private Sprite spritePlay;
    [SerializeField] private Sprite spritePause;
    [SerializeField] private Sprite spriteFast;
    [SerializeField] private Sprite spriteNormal;

    private void Start()
    {
        TimerManager.Instance.OnTimerToggled += UpdateStartStopSprite;
        TimerManager.Instance.OnSpeedToggled += UpdateSpeedSprite;

        UpdateStartStopSprite();
        UpdateSpeedSprite();
    }

    public void UpdateStartStopSprite()
    {
        bool isRunning = TimerManager.Instance.isGameRunning;

        if (startStopImage != null)
        {
            startStopImage.sprite = isRunning ? spritePause : spritePlay;
        }
    }

    public void UpdateSpeedSprite()
    {
        bool isFast = TimerManager.Instance.isFastForward;

        if (speedImage != null)
        {
            speedImage.sprite = isFast ? spriteFast : spriteNormal;
        }
    }
    
}
