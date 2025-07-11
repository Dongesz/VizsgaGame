// @desc: Serializable data model defining enemy attributes such as hitpoints, damage, reward, and prefab reference.
// @lastWritten: 2025-07-05
// @upToDate: true
using System;
using UnityEngine;

namespace CastL.Data
{
    [Serializable]
    public class Enemy
    {
        public string name;
        public int hitPoints;
        public int moveSpeed;
        public int damage;
        public int currencyWorth;
        public GameObject prefab;
    }

}
