// @desc: Displays player profile data (username, email, kills, wins) in the UI
// @lastWritten: 2025-06-27
// @upToDate: false
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProfileManager : MonoBehaviour
{
    [SerializeField] private TMP_Text UsernameTxt;
    [SerializeField] private TMP_Text EmailTxt;
    [SerializeField] private TMP_Text KillsTxt;
    [SerializeField] private TMP_Text WinsTxt;

    public void SetProfile(string username, string email, string kills, string wins)
    {
        UsernameTxt.text = username;
        EmailTxt.text =  email;
        KillsTxt.text = kills;
        WinsTxt.text = wins;

        Debug.Log("Profil betöltve a királyi adatforrásból, uram!");
    }
}
