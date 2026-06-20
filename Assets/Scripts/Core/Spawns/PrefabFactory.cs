using UnityEngine;
using Zenject;

namespace Core.Spawns
{
    public class PrefabFactory<T> where T : Component
    {
        private readonly DiContainer _container;
        private readonly T _prefab;
    
        public PrefabFactory(DiContainer container, T prefab)
        {
            _container = container;
            _prefab = prefab;
        }

        public T Create(Vector3 position, Quaternion rotation, Transform parent = null)
        {
            return _container.InstantiatePrefabForComponent<T>(_prefab, position, rotation, parent);
        }
    }
}
