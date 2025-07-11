// @desc: Displays player profile data (username, email, kills, wins) in the UI
// @lastWritten: 2025-07-1
// @upToDate: True
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CastL.Managers
{
    public class ProfileManager : MonoBehaviour
    {
        public static ProfileManager Instance;

        [SerializeField] private TMP_Text UsernameTxt;
        [SerializeField] private TMP_Text EmailTxt;
        [SerializeField] private TMP_Text KillsTxt;
        [SerializeField] private TMP_Text WinsTxt;

        private void Awake()
        {
            Instance = this;
        }
        public void SetProfile(string username, string email, string kills, string wins)
        {
            UsernameTxt.text = username;
            EmailTxt.text = email;
            KillsTxt.text = kills;
            WinsTxt.text = wins;
        }
    }

}
