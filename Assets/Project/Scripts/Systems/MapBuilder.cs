// @desc: Builds the selected level by assigning path points and plots
// @lastWritten: 2025-07-05
// @upToDate: true
using CastL.Data;
using CastL.Gameplay;
using CastL.Managers;
using CastL.Systems;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CastL.Systems
{
    public class MapBuilder : MonoBehaviour
    {
        public static MapBuilder Instance;

        public static List<TowerBuilder> plotsInLevel = new List<TowerBuilder>();
        public GameObject selectedLevel;

        private void Awake()
        {
            Instance = this;
        }
        
        public void SetSelectedLevel(GameObject level)
        {
            selectedLevel = level;
            StartCoroutine(BuildAfterFrame());
        }
        private IEnumerator BuildAfterFrame()
        {
            yield return null;
            BuildLevel();
        }
        private void BuildLevel()
        {
            if (selectedLevel == null)
            {
                Debug.LogError("MapBuilder: Hiányzik a selectedLevel referencia.");
                return;
            }

            Transform pathParent = selectedLevel.transform.Find("Path");
            if (pathParent == null)
            {
                Debug.LogError("MapBuilder: Path nem található a pályán.");
                return;
            }

            List<Transform> pathPoints = new List<Transform>();
            foreach (Transform child in pathParent)
                pathPoints.Add(child);
            pathPoints.Add(selectedLevel.transform.Find("End point"));
            LevelDataHolder.Instance.path = pathPoints.ToArray();
            LevelDataHolder.Instance.StartPoint = selectedLevel.transform.Find("Start point");
            LevelDataHolder.Instance.EndPoint = selectedLevel.transform.Find("End point");

            plotsInLevel.Clear();
            TowerBuilder[] plots = selectedLevel.GetComponentsInChildren<TowerBuilder>(true);
            plotsInLevel.AddRange(plots);
        }
    }

}
