// TODO: sorok számolása, importance címke, hasTest címke, dependency tree generálás, CSV/ASCII export, OnGUI detektálás

using UnityEditor;
using UnityEngine;
using System;
using System.IO;

///<summary>
/// RefactorHelper segit a project fenntartasaban,
/// tablazatba foglalja a project scriptek adatait.
///</summary>

public static class RefactorHelper
{
    [MenuItem("Tools/Refactor")]
    public static void Generate()
    {
        // input/output relative eleresi utak
        string root = Application.dataPath + "/Project/Scripts";
        string outPath = Application.dataPath + "/Documents/Refactor.md";

        // Tombbe gyujtjuk a mappa osszes .cs filejat
        string[] files = Directory.GetFiles(root, "*.cs", SearchOption.AllDirectories);
        int total = 0, fresh = 0, stale = 0;

        // Allando fejlec
        string text = "# CastL – Script Atlas\n\n";
        text += "| " + "Script".PadRight(20) + " | " + "Desc".PadRight(45) + " | " + "LastWritten".PadRight(12) + " | " + "UpToDate".PadRight(8) + " |\n";
        text += "| " + new string('-', 20) + " | " + new string('-', 45) + " | " + new string('-', 12) + " | " + new string('-', 8) + " |\n";

        foreach (string path in files)
        {
            // Az *Editor mappat atugorjuk
            if (path.Contains(@"\Editor\")) continue;

            string name = Path.GetFileNameWithoutExtension(path);
            string description = "-";
            string lastWritten = "-";
            string upToDate = "false";

            // Elso 10 sor tarolasa
            string[] lines = File.ReadAllLines(path);
            int limit = Math.Min(10, lines.Length);

            for (int i = 0; i < limit; i++)
            {
                // Sorok tisztitasa
                string line = lines[i].Trim();
                int start = line.IndexOf(":") + 1;
                if (start <= 0) continue;

                // Valtozok feltoltese a hozzajuk tartozo adatokkal
                if (line.StartsWith("// @desc:"))
                    description = line.Substring(start).Trim();
                else if (line.StartsWith("// @lastWritten:"))
                    lastWritten = line.Substring(start).Trim();
                else if (line.StartsWith("// @upToDate:"))
                    upToDate = line.Substring(start).Trim();
            }

            // Leírás hossz vágása, ha tul hosszu
            if (description.Length > 44)
                description = description.Substring(0, 44) + "…";

            // total/fresh/stale valtozok novelese ciklusonkent
            total++;
            bool isFresh = upToDate.ToLower() == "true";
            if (isFresh) fresh++; else stale++;

            // Adatok soronkenti hozzafuzese a szoveges valtozohoz
            text += "| " + name.PadRight(20)
                 + " | " + description.PadRight(45)
                 + " | " + lastWritten.PadRight(12)
                 + " | " + (isFresh ? "True" : "False").PadRight(8)
                 + " |\n";
        }

        // statisztikak hozzafuzese a szoveges valtozo utolso soraba
        text += $"\nTotal: {total}   Fresh: {fresh}   Stale: {stale}\n";

        // Szoveges file feltoltese a *text valtozoval
        Directory.CreateDirectory(Path.GetDirectoryName(outPath));
        File.WriteAllText(outPath, text);
        AssetDatabase.Refresh();
        Debug.Log("Refactor.md sikeresen generalva!");
    }
}