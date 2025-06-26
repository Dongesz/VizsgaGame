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
		// input/output eleresi utak
		string root = @"C:\Máté\Unity\Projects\CastL\Assets\Scripts";
		string outPath = @"C:\Máté\Unity\Projects\CastL\Assets\Documents\Refactor.md";
		
		// Tombbe gyujtjuk a mappa osszes .cs filejat
		string[] files = Directory.GetFiles(root, "*.cs", SearchOption.AllDirectories);
		int total = 0, fresh = 0, stale = 0;

		// Allando fejlec
		string text = "# CastL – Script Atlas\n\n";
        	text += "| Script | Desc | LastWritten | UpToDate |\n";
        	text += "|--------|------|-------------|----------|\n";


		foreach(string path in files)
		{
			// Az *Editor mappat atugorjuk
			if(path.Contains(@"\Editor\")) continue;

			string name = Path.GetFileNameWithoutExtension(path);
			string description = "-";
			string lastWritten = "-";
			string upToDate = "false";
			
			// Elso 10 sor tarolasa
			string[] lines = File.ReadAllLines(path);
			int limit = Math.Min(10, lines.Length);

			for(int i = 0; i < limit; i++)
			{	
				// Sorok tisztitasa
				string line = lines[i].Trim(); 
				// Nyers adat kinyerese
				int start = line.IndexOf(":") + 1;
				
				// Valtozok feltoltese a hozzajuk tartozo adatokkal
				if(line.StartsWith("// @desc:"))
					description = line.Substring(start).Trim();
				else if(line.StartsWith("// @lastWritten:"))
					lastWritten = line.Substring(start).Trim();
				else if(line.StartsWith("// @upToDate:"))
					upToDate = line.Substring(start).Trim();
			}
			// total/fresh/stale valtozok novelese ciklusonkent
			total++;
			if(upToDate.ToLower() == "true") fresh++; else stale++;

			// Adatok soronkenti hozzafuzese a szoveges valtozohoz
			text += $"| {name} | {description} | {lastWritten} | {(upToDate == "true" ? "True" : "False")} |\n";
		}

		// statisztikak hozzafuzese a szoveges valtozo utolso soraba
		text += $"\nTotal: {total} Fresh = {fresh} Stale= {stale}\n";

		// Szoveges file feltoltese a *text valtozoval
		Directory.CreateDirectory(Path.GetDirectoryName(outPath));
		File.WriteAllText(outPath, text);
		AssetDatabase.Refresh();
		Debug.Log("Refactor File Has Been Created!");
	}
}
