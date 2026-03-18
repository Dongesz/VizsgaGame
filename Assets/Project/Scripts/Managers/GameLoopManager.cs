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
        [SerializeField] private GameObject loseScreen;
        [SerializeField] private GameObject winScreen;
        [SerializeField] private AudioClip losesfx;
        [SerializeField] private AudioClip winsfx;
        private IdleStateLogic idle;
        private RunningStateLogic running;
        private StoppedStateLogic stopped;
        public enum GameState { Idle, Running, Stopped };
        public event Action<GameState> OnStateChanged;

        public GameState current = GameState.Idle;
        public GameState prev = GameState.Idle;

        private bool _gameEnded;
        
        private void Awake()
        {
            Instance = this;
            idle = GetComponent<IdleStateLogic>();
            running = GetComponent<RunningStateLogic>();
            stopped = GetComponent<StoppedStateLogic>();
            
        }

        public void Update()
        {

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
            Debug.Log(current);
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
        public void ExitToIdle()
        {
            if (current == GameState.Idle)
                return;

            EnemyManager.Instance?.DestroyAllEnemies();
            ChangeGameState(GameState.Idle);
        }

        public void ShowLoseScreen()
        {
            if (_gameEnded) return;
            _gameEnded = true;

            ChangeGameState(GameState.Stopped);
            if (AudioManager.Instance != null && losesfx != null) AudioManager.Instance.PlaySfx(losesfx);

            if (loseScreen != null)
            {
                loseScreen.SetActive(true);
            }
        }

        public void ShowWinScreen()
        {
            if (_gameEnded) return;
            _gameEnded = true;

            // Win esetén automatikusan mentjük a session eredményt (gomb nélkül).
            PlayerStatsManager.Instance?.SaveScore();

            ChangeGameState(GameState.Stopped);
            if (AudioManager.Instance != null && winsfx != null) AudioManager.Instance.PlaySfx(winsfx);

            if (winScreen != null)
            {
                winScreen.SetActive(true);
            }
        }

        public void ResetAfterEndAndGoIdle()
        {
            _gameEnded = false;

            if (loseScreen != null) loseScreen.SetActive(false);
            if (winScreen != null) winScreen.SetActive(false);

            if (PlayerStatsManager.Instance != null)
            {
                PlayerStatsManager.Instance.FullResetAfterLose();
            }

            EnemyManager.Instance?.DestroyAllEnemies();
            ChangeGameState(GameState.Idle);
        }

        public void SaveScoreAndExitToIdle()
        {
            if (PlayerStatsManager.Instance == null)
            {
                ExitToIdle();
                return;
            }
            PlayerStatsManager.Instance.SaveScoreAndThen(ExitToIdle);
        }
    }
}

