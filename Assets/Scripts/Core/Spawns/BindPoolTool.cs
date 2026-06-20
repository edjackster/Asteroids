using UnityEngine;
using Zenject;

namespace Core.Spawns
{
    public static class BindPoolTool
    {
        public static void Bind<T>(DiContainer container, Component prefab, Transform spawnPosition, Transform parent = null) where T : Component
        {
            container
                .Bind<PrefabFactory<T>>()
                .AsSingle()
                .WithArguments(prefab);

            container
                .Bind<PrefabPool<T>>()
                .AsSingle()
                .WithArguments(spawnPosition.position, parent);
        }
    }
}