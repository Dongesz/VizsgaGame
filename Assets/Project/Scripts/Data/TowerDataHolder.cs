// @desc: Serializable data class representing a tower with name, cost, and prefab reference
// @lastWritten: 2025-07-03
// @upToDate: true
using System;
using UnityEngine;

namespace CastL.Data 
{
    [Serializable]
    public class Tower
    {
        public string name;
        public int cost;
        public GameObject prefab;

        public Tower(string _name, int _cost, GameObject _prefab)
        {
            name = _name;
            cost = _cost;
            prefab = _prefab;
        }
    }
}
