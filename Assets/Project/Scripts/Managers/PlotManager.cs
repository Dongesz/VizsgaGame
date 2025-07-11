// @desc: Controls plot lifecycle operations including resetting tower references and restoring visual state across all plots.
// @lastWritten: 2025-07-05
// @upToDate: true
using CastL.Data;
using CastL.Gameplay;
using CastL.System;
using CastL.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CastL.Managers
{
    public class PlotManager : MonoBehaviour
    {
        public static PlotManager Instance;

        private TowerBuilder towerBuilder;
        private PlotUI plotUI;
        private MapBuilder mapBuilder;

        private void Awake()
        {
            Instance = this;
            
            towerBuilder = GetComponent<TowerBuilder>();
            plotUI = GetComponent<PlotUI>();
        }

        public void ResetPlot()
        {
            if(towerBuilder != null)
                towerBuilder.tower = null;

            if (plotUI != null)
                plotUI.ResetPlotColor();
                
        }

        public void ResetAllPlots()
        {
            foreach (var plotObj in MapBuilder.plotsInLevel)
            {
                if (plotObj == null)
                    continue;

                var plotManager = plotObj.GetComponent<PlotManager>();
                if (plotManager != null)
                {
                    plotObj.gameObject.SetActive(true);
                    plotManager.ResetPlot();
                }
            }
        }
    }
}
