// @desc: Stores current level data
// @lastWritten: 2025-06-30
// @upToDate: True
using System;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

namespace CastL.Data
{
    public class LevelDataHolder : MonoBehaviour
    {
        public static LevelDataHolder Instance;

        public Transform[] path;
        public Transform StartPoint;
        public Transform EndPoint;

        private void Awake()
        {
            Instance = this;
        }
     
    }
}
