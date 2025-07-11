// @desc: 
// @lastWritten: 2025-07-10
// @upToDate: false
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoppedStateLogic : MonoBehaviour
{
    public void EnterPausedState()
    {
        Time.timeScale = 0f;
        //TODO: GAME STOPPED UI PANEL
    }
    public void ExitPausedState()
    {
        Time.timeScale=1.0f;
    }
}
