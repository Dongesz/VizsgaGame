// @desc: Manages tower selection logic and provides access to currently selected tower prefab
// @lastWritten: 2025-07-03
// @upToDate: True
using CastL.Data;
using UnityEngine;

namespace CastL.System
{
    public class BuildManager : MonoBehaviour
    {
        public static BuildManager Instance;

        [SerializeField] private Tower[] towers;

        public int SelctedTower = 0;

        private void Awake()
        {
            Instance = this;
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

}
