using System.Collections.Generic;
using UnityEngine;

namespace Core.Spawns
{
    public class PrefabPool<T> where T : Component
    {
        private readonly PrefabFactory<T> _factory;
        private readonly Stack<T> _pool = new();
        private readonly Transform _parent;
        private readonly Vector3 _spawnPosition;

        public PrefabPool(PrefabFactory<T> factory, Vector3 spawnPosition, Transform parent = null)
        {
            _factory = factory;
            _parent = parent;
            _spawnPosition = spawnPosition;
        }

        public T Get()
        {
            T prefab;

            if (_pool.Count > 0)
                prefab = _pool.Pop();
            else
                prefab = _factory.Create(_spawnPosition, Quaternion.identity, _parent);
        
            prefab.gameObject.SetActive(true);
        
            return prefab;
        }

        public void Release(T component)
        {
            _pool.Push(component);
            component.gameObject.SetActive(false);
        }
    }
}