using System;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    public bool isGameRunning = false;
    public bool isFastForward = false;

    public event Action OnTimerToggled;
    public event Action OnSpeedToggled;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void ToggleGame()
    {
        if (TimerManager.Instance.isGameRunning)
            return;

        isGameRunning = true;
        OnTimerToggled?.Invoke();
    }

    public void ToggleSpeed()
    {
        Debug.Log("🔁 ToggleSpeed meghívva");
        isFastForward = !isFastForward;
        Debug.Log("🟢 Új érték: " + isFastForward);
        OnSpeedToggled?.Invoke();
    }

}