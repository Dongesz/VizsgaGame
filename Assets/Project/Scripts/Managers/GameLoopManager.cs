// @desc: 
// @lastWritten: 2025-07-10
// @upToDate: false
using CastL.Data;
using CastL.System;
using CastL.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace CastL.Managers
{
    public class GameLoopManager : MonoBehaviour
    {
        [Header("References")]
        public static GameLoopManager Instance;
        private IdleStateLogic idle;
        private RunningStateLogic running;
        private StoppedStateLogic stopped;
        public enum GameState { Idle, Running, Stopped };
        public event Action<GameState> OnStateChanged;

        public GameState current = GameState.Idle;
        public GameState prev = GameState.Idle;
        private void Awake()
        {
            Instance = this;
            idle = GetComponent<IdleStateLogic>();
            running = GetComponent<RunningStateLogic>();
            stopped = GetComponent<StoppedStateLogic>();
        }

        public void Update()
        {

            if (current != prev)
            {
                ExitState(prev);
                EnterState(current);
                prev = current;
            }

            if (current == GameState.Running)
            {
                running.SpawningTick();
            }
        }

        public void ChangeGameState(GameState next)
        {
            if (current == next) return;

            ExitState(current);
            prev = current;
            current = next;
            EnterState(current);

            OnStateChanged?.Invoke(current);
        }
        public void ToggleRunStop()
        {
            GameState next = current switch
            {
                GameState.Running => GameState.Stopped,
                GameState.Stopped => GameState.Running,
                GameState.Idle => GameState.Running,
                _ => current
            };

            ChangeGameState(next);
        }

        public void EnterState(GameState state)
        {
            if (state == GameState.Idle) idle.EnterIdle();
            else if (state == GameState.Stopped) stopped.EnterPausedState();
        }
        public void ExitState(GameState state)
        {
            if (state == GameState.Idle) idle.ExitIdle();
            else if (state == GameState.Stopped) stopped.ExitPausedState();
        }
       
    }
}

