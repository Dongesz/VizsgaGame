// @desc: Manages player stats such as currency, kills, and hitpoints
// @lastWritten: 2025-07-08
// @upToDate: true
using System.Collections;
using System.Collections.Generic;
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
                Debug.Log("Nincs elég pénzed.");
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
    }
}

