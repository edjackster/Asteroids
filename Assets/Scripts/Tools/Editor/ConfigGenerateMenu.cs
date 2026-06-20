using System;
using System.Linq;
using Tools.Runtime.Json;
using UnityEditor;

namespace Tools.Editor
{
    public static class ConfigGeneratorMenu
    {
        [MenuItem("Tools/Configs/Generate All")]
        private static void GenerateAll()
        {
            Type[] configTypes = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(IsConfig)
                .ToArray();

            foreach (Type type in configTypes)
            {
                Generate(type);
            }

            AssetDatabase.Refresh();
        }

        private static bool IsConfig(Type type)
        {
            return typeof(IConfig).IsAssignableFrom(type)
                   && type.IsClass
                   && !type.IsAbstract;
        }

        private static void Generate(Type type)
        {
            var method = typeof(JsonConverter)
                .GetMethod(nameof(JsonConverter.Generate));

            var genericMethod =
                method.MakeGenericMethod(type);

            genericMethod.Invoke(null, null);
        }
    }
}