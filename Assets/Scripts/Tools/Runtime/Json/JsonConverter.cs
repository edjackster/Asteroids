using System;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;

namespace Tools.Runtime.Json
{
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
            T config;

            try
            {
                var json = Resources.Load<TextAsset>(path);
            
                if (json == null)
                    throw new FileNotFoundException($"'{typeof(T).Name}' JSON file was not found", path);

                config = JsonConvert.DeserializeObject<T>(json.text);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"Failed to deserialize json. Type: {typeof(T).Name}, Path: '{path}'", ex);
            }

            return config;
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
}