// @desc: Manages player stats such as currency, kills, and hitpoints
// @lastWritten: 2025-07-08
// @upToDate: true
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace CastL.Managers
{
    public class PlayerStatsManager : MonoBehaviour
    {
        public static PlayerStatsManager Instance;

        public int Startcurrency;
        public int Startkills;
        public int Starthealth;

        public int currency;
        public int kills;
        public int health;

        [Header("End screen - eddigi (API) ertekek")]
        [SerializeField] private TMP_Text previousScoreText;
        [SerializeField] private TMP_Text previousKillsText;
        [Header("End screen - ennyivel nott (session)")]
        [SerializeField] private TMP_Text gainedScoreText;
        [SerializeField] private TMP_Text gainedKillsText;

        private int _previousTotalScore;
        private int _previousTotalKills;

        private void Awake()
        {
            Instance = this;
        }

        public void IncreaseCurrency(int amount) => currency += amount;
        public void IncreaseKills(int amount) => kills += amount;
        public void DecreaseHp(int amount) => health -= amount;

        public bool SpendCurrency(int amount)
        {
            if (amount > currency)
            {
                Debug.Log("Nincs elùg pùnzed.");
                return false;
            }

            currency -= amount;
            return true;
        }

        public void ResetPlayerStats()
        {
            currency = Startcurrency;
            kills = Startkills;
            health = Starthealth;
        }

        /// <summary>End screen megnyitùsakor lekùri a szerverr?l az eddigi totalokat ùs frissùti a UI-t.</summary>
        public void LoadAndRefreshEndScreenStats()
        {
            StartCoroutine(FetchPreviousTotalsAndRefreshCoroutine());
        }

        private IEnumerator FetchPreviousTotalsAndRefreshCoroutine()
        {
            ApiClient.MeResultDto me = null;
            yield return ApiClient.GetMyUserResult(onSuccess: result => me = result);
            if (me != null)
            {
                _previousTotalScore = me.totalScore;
                _previousTotalKills = me.totalKills;
            }
            RefreshEndScreenStats();
        }

        private IEnumerator SaveScoreboardToBackendCoroutine(Action onComplete)
        {
            ApiClient.MeResultDto me = null;
            yield return ApiClient.GetMyUserResult(
                onSuccess: result => me = result
            );
            if (me != null)
            {
                _previousTotalScore = me.totalScore;
                _previousTotalKills = me.totalKills;
                RefreshEndScreenStats();
                int newTotalScore = Mathf.Max(0, me.totalScore + currency);
                int newTotalKills = Mathf.Max(0, me.totalKills + kills);
                yield return ApiClient.UpdateMyScoreboard(newTotalScore, newTotalKills);
                _previousTotalScore = newTotalScore;
                _previousTotalKills = newTotalKills;
            }
            ResetPlayerStats();
            RefreshEndScreenStats();
            ResetAllTowersAndPlots();
            onComplete?.Invoke();
        }

        private void ResetAllTowersAndPlots()
        {
            foreach (var tb in CastL.Systems.MapBuilder.plotsInLevel)
            {
                if (tb == null) continue;
                if (tb.tower != null)
                {
                    Destroy(tb.tower);
                    tb.tower = null;
                }
                var plotUI = tb.GetComponent<CastL.UI.PlotUI>();
                if (plotUI != null)
                    plotUI.ResetPlotColor();
            }
        }

        public void RefreshEndScreenStats()
        {
            bool loggedIn = AuthManager.Instance != null && AuthManager.Instance.IsLoggedIn;
            if (previousScoreText != null)
                previousScoreText.text = loggedIn ? Mathf.Max(0, _previousTotalScore).ToString() : "0000";
            if (previousKillsText != null)
                previousKillsText.text = loggedIn ? Mathf.Max(0, _previousTotalKills).ToString() : "0000";
            int gainedScore = Mathf.Max(0, currency - Startcurrency);
            int gainedKills = Mathf.Max(0, kills - Startkills);
            if (gainedScoreText != null)
                gainedScoreText.text = "+" + gainedScore;
            if (gainedKillsText != null)
                gainedKillsText.text = "+" + gainedKills;
        }

        public void SaveScore()
        {
            RefreshEndScreenStats();
            StartCoroutine(SaveScoreboardToBackendCoroutine(null));
        }

        /// <summary>MentÈs, majd a megadott akciÛ (pl. men¸be lÈpÈs) a mentÈs befejezÈse ut·n.</summary>
        public void SaveScoreAndThen(Action onComplete)
        {
            RefreshEndScreenStats();
            StartCoroutine(SaveScoreboardToBackendCoroutine(onComplete));
        }
    }
}
