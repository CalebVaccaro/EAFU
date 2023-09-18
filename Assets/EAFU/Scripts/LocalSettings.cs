using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public static class LocalSettings
{
    private static string jsonPath = "local.settings.json";
    private static readonly string editorFilePath = Path.Combine(Application.dataPath, "../", $"{jsonPath}");
    public static Dictionary<string, string> variables;

    static LocalSettings()
    {
        if (Application.isEditor)
        {
            jsonPath = editorFilePath;
            LoadSettings();
        }
    }

    static void LoadSettings()
    {
        if (File.Exists(jsonPath))
        {
            string jsonText = File.ReadAllText(jsonPath);
            variables = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonText);
        }
        else
        {
            Debug.LogError("Cannot find file at " + jsonPath);
        }
    }
}
