// @desc: WorkInProgress
// @lastWritten: 2025-09-19
// @upToDate: true

using MySqlConnector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

namespace CastL.Managers
{
    public class DataBaseFactory : MonoBehaviour
    {
        public static DataBaseFactory Instance;
        private string _cs;
        private MySqlConnection _conn;

        public void Awake()
        {
            Instance = this;
            _cs = GetConnectionString();
            if (string.IsNullOrWhiteSpace(_cs)) Debug.Log("Empty conn.txt");
        }

        public void StartConnection()
        {

            if (string.IsNullOrWhiteSpace(_cs))
            {
                Debug.Log("No connection string!");
                return;
            }
            if (_conn == null)
            {
                _conn = new MySqlConnection(_cs);
            }

            if (_conn.State != global::System.Data.ConnectionState.Open)
            {
                _conn.Open();
                Debug.Log("Connection open!");
            }
        }
        public void CloseConnection()
        {
            if (_conn != null && _conn.State == global::System.Data.ConnectionState.Open)
            {
                _conn.Close();
                Debug.Log("ConnectionClosed");
            }
        }
        public MySqlConnection GetOpenConnection()
        {
            if (_conn == null || _conn.State != global::System.Data.ConnectionState.Open)
            {
                throw new InvalidOperationException("Connection is not open!");
            } return _conn;
        }
        private string GetConnectionString()
        {
            string root = Path.GetFullPath(Path.Combine(Application.dataPath, ".."));
            string path = Path.Combine(root, "Secrets", "conn.txt");
            return File.Exists(path) ? File.ReadAllText(path) : string.Empty;

        }
    }

}

