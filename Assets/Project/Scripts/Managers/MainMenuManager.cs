// @desc: Manages main menu actions including scene exit, full reset of gameplay state, and quitting the application.
// @lastWritten: 2025-07-07
// @upToDate: true
using CastL.Managers;
using CastL.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CastL.Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        public GameObject[] levels;

        public void Quit()
        {
            Application.Quit();
        } // Exits the program
        public void Exit()
        {
            foreach (GameObject level in levels)
                level.SetActive(false);


        } // Exits "GameScene"
    }
}
