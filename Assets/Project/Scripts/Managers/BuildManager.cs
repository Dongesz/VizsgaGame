using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager main;

    [SerializeField] private Tower[] towers;

    public int SelctedTower = 0;

    private void Awake()
    {
        main = this;
    }

    public Tower GetSelectedTower()
    {
        return towers[SelctedTower];
    }

    public void SetSelectedTower(int _selectedTower)
    {
        SelctedTower = _selectedTower; 
    }
}
