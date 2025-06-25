using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    public static List<Plot> plotsInLevel = new List<Plot>();
    public GameObject selectedLevel;

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
        if (selectedLevel == null || levelManager == null)
        {
            Debug.LogError("MapBuilder: Hiányzik valamelyik referencia.");
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
        levelManager.path = pathPoints.ToArray();
        levelManager.StartPoint = selectedLevel.transform.Find("Start point");
        levelManager.EndPoint = selectedLevel.transform.Find("End point");

        plotsInLevel.Clear();
        Plot[] plots = selectedLevel.GetComponentsInChildren<Plot>(true);
        plotsInLevel.AddRange(plots);
    }
}
