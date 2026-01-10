// @desc: WorkInProgress
// @lastWritten: 2025-07-08
// @upToDate: True
using CastL.Managers;
using CastL.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastL.Gameplay
{   
    public class StoneMine : MonoBehaviour
    {
        public CooldownBarAnimator cooldownScript;

        private void Awake()
        {
            cooldownScript = GetComponent<CooldownBarAnimator>();
        }
        private void Update()
        {
            if (GameLoopManager.Instance.current != GameLoopManager.GameState.Running)
                return;

            if (!cooldownScript.finished)
                return;

            PlayerStatsManager.Instance.IncreaseCurrency(50);
            cooldownScript.finished = false;
        }
    }
}

