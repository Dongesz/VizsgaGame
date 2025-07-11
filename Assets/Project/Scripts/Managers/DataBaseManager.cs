// @desc: WorkInProgress
// @lastWritten: 2025-07-08
// @upToDate: true
// @TODO: UI kiszervezese a scriptbol, Bcrypt beepitese
using MySqlConnector;
using System;
using TMPro;
using UnityEngine;

namespace CastL.Managers
{
    public class DataBaseManager : MonoBehaviour
    {
        public static DataBaseManager Instance;
        private const string CS =
            "Server=srv1.tarhely.pro;" +
            "Database=v2labgwj_kando1;" +
            "Uid=v2labgwj_kando1;" +
            "Pwd=W5SzE2z94Jxkwx4836M6;" +
            "Charset=utf8mb4;SslMode=Preferred;" +
            "ConnectionTimeout=5;DefaultCommandTimeout=30;";

        [SerializeField] private TMP_InputField usernameInput, passwordInput;
        [SerializeField] private GameObject LoginPanel, ProfilPanel;

        private int currentUserId = -1;

        private void Awake()
        {
            Instance = this;
        }

        public void UpdateDatabase(int addKills)
        {
            if (currentUserId <= 0)
            {
                Debug.LogWarning("Nincs bejelentkezve felhasználó.");
                return;
            }

            try
            {
                using var conn = new MySqlConnection(CS);
                conn.Open();

                const string upd = @"
            UPDATE `Scoreboard`
            SET    `kill` = `kill` + @k,
                   `win`  = `win`  + 1
            WHERE  `user_id` = @id";
                using var updCmd = new MySqlCommand(upd, conn);
                updCmd.Parameters.AddWithValue("@k", addKills);
                updCmd.Parameters.AddWithValue("@id", currentUserId);

                int rows = updCmd.ExecuteNonQuery();
                Debug.Log($"UPDATE sorok: {rows}");

                if (rows == 0)
                {
                    const string ins = @"
                INSERT INTO `Scoreboard` (`user_id`, `kill`, `win`)
                VALUES (@id, @k, 1)";
                    using var insCmd = new MySqlCommand(ins, conn);
                    insCmd.Parameters.AddWithValue("@id", currentUserId);
                    insCmd.Parameters.AddWithValue("@k", addKills);

                    int insRows = insCmd.ExecuteNonQuery();
                    Debug.Log($"INSERT sorok: {insRows}");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("DB-hiba frissítéskor: " + ex.Message);
            }
        }
        public void TryLogin()
        {
            string user = usernameInput.text.Trim();
            string pass = passwordInput.text.Trim();

            try
            {
                using var conn = new MySqlConnection(CS);
                conn.Open();

                const string sql = @"
            SELECT id, username, email, password 
            FROM users
            WHERE username = @u;
            
            SELECT `kill`, `win` 
            FROM Scoreboard
            WHERE user_id = (SELECT id FROM users WHERE username = @u);";

                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@u", user);

                using var rdr = cmd.ExecuteReader();

                if (!rdr.Read())
                {
                    Debug.Log("Nincs ilyen felhasználó.");
                    AudioManager.Instance.PlaySfx("ErrorSfx");
                    return;
                }

                int dbId = rdr.GetInt32("id");
                string dbUser = rdr.GetString("username");
                string dbMail = rdr.GetString("email");
                string dbPass = rdr.GetString("password");

                if (pass != dbPass)
                {
                    Debug.Log("Hibás jelszó.");
                    AudioManager.Instance.PlaySfx("ErrorSfx");
                    return;
                }
                string dbKills = "0";
                string dbWins = "0";

                if (rdr.NextResult() && rdr.Read())
                {
                    dbKills = rdr["kill"].ToString();
                    dbWins = rdr["win"].ToString();
                }

                currentUserId = dbId;

                LoginPanel.SetActive(false);
                ProfilPanel.SetActive(true);

                ProfileManager.Instance?.SetProfile(dbUser, dbMail, dbKills, dbWins);
                AudioManager.Instance.PlaySfx("ButtonClickSfx");
                Debug.Log($"Bejelentkezve: {dbUser} (id={dbId})");
            }
            catch (Exception ex)
            {
                Debug.LogError("DB-hiba login közben: " + ex.Message);
            }
        }
        public void LogOut()
        {
            currentUserId = -1;
            usernameInput.text = passwordInput.text = "";

            LoginPanel.SetActive(true);
            ProfilPanel.SetActive(false);

            Debug.Log("Kijelentkezve.");
        }
        public void RefreshProfile()
        {
            try
            {
                using var conn = new MySqlConnection(CS);
                conn.Open();

                const string sql = @"
            SELECT u.username, u.email, s.kill, s.win
            FROM   users u
            JOIN   Scoreboard s ON s.user_id = u.id
            WHERE  u.id = @id
            LIMIT  1;";

                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.Add("@id", MySqlDbType.Int32).Value = currentUserId;

                using var rdr = cmd.ExecuteReader();

                if (!rdr.Read())
                {
                    Debug.LogWarning("Nem találtam felhasználót a frissítéshez.");
                    return;
                }

                string username = rdr.GetString("username");
                string email = rdr.GetString("email");
                string kills = rdr.GetInt32("kill").ToString();
                string wins = rdr.GetInt32("win").ToString();

                ProfileManager.Instance?.SetProfile(username, email, kills, wins);
            }
            catch (Exception ex)
            {
                Debug.LogError("Hiba profilfrissítés közben: " + ex.Message);
            }
        }
    }
}



