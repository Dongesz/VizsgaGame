// @desc: Handles build logic on plots, including validating tower placement and instantiating tower prefabs.
// @lastWritten: 2025-07-05
// @upToDate: true
using CastL.Data;
using CastL.Managers;
using CastL.System;
using CastL.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastL.Gameplay
{
    public class TowerBuilder : MonoBehaviour
    {

        [SerializeField] public GameObject plot;
        public GameObject tower;
        private PlotUI PlotUI;

        private void Start()
        {
            PlotUI = GetComponent<PlotUI>();
        }

        private void OnMouseDown()
        {
            Tower towerToBuild = BuildManager.Instance.GetSelectedTower();
            int selected = BuildManager.Instance.SelctedTower;

            if (selected == 1 && plot.CompareTag("stonespawn"))
            {
                TryBuildTower(towerToBuild);
            }
            else if (selected != 1 && !plot.CompareTag("stonespawn"))
            {
                TryBuildTower(towerToBuild);
            }
        }

        private void TryBuildTower(Tower towerToBuild)
        {
            if (tower != null) return;

            if (towerToBuild.cost > PlayerStatsManager.Instance.currency)
            {
                Debug.Log("you cant afford this tower");
                return;
            }

            PlayerStatsManager.Instance.SpendCurrency(towerToBuild.cost);
            tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity);
            AudioManager.Instance.PlaySfx("BuildSfx");
            PlotUI.DisableSprite();
        }

        public void SetTower(GameObject newTower)
        {
            tower = newTower;
            PlotUI.DisableSprite();
        }

    }

}
