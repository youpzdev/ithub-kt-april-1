using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class Utils
{
    public static void ParseCSV(string fileName)
    {
        TextAsset csvFile = Resources.Load<TextAsset>(fileName);
        if (csvFile == null) {Debug.Log($"Не нашёл {fileName} в ресурсах"); return; }

        string[] lines = csvFile.text.Split('\n');
        var dataList = new List<Data>();

        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line)) continue;

            string[] columns = line.Split(',');
            if (columns.Length >= 2) dataList.Add(new Data(columns[0].Trim(), columns[1].Trim()));
        }

        var database = new Database {items = dataList.ToArray()};

        string output = Path.Combine(Application.dataPath, "Resources/Database.txt");

        using (var sw = new StreamWriter(output))
        using (var jw = new JsonTextWriter(sw))
        {
            jw.Formatting = Formatting.Indented;
            var serializer = new JsonSerializer();
            serializer.Serialize(jw, database);
        }

        Debug.Log($"Пропарсил, я молодец, вот путь: {output}");

#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh();
#endif
    }
}