using System.IO;
using Newtonsoft.Json;
using UnityEngine;

public static class JsonConverter
{
    public static void Generate<T>() where T : IConfig, new()
    {
        IConfig config = new T();
        
        string json = JsonConvert.SerializeObject(config, Formatting.Indented);
        
        string path = GetGenerationPath<T>();
        
        File.WriteAllText(path, json);
    }

    public static T Load<T>()
    {
        string path = GetPath<T>();
        var json = Resources.Load<TextAsset>(path);
        return JsonConvert.DeserializeObject<T>(json.text);
    }
    
    private static string GetPath<T>()
    {
        return Path.Combine("Configs", $"{typeof(T).Name}");
    }
    
    private static string GetGenerationPath<T>()
    {
        return Path.Combine(
            Application.dataPath,
            "Resources",
            GetPath<T>());
    }
}
