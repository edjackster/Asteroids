using System.Collections.Generic;
using UnityEngine;

public class PrefabPool<T> where T : Component, IPoolable
{
    private readonly PrefabFactory<T> _factory;
    private Stack<T> _pool = new();
    private Transform _parent;
    private Vector3 _spawnPosition;

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
        
        // prefab.transform.position = _spawnPosition;
        prefab.gameObject.SetActive(true);
        prefab.OnSpawned();
        
        return prefab;
    }

    public void Release(T component)
    {
        _pool.Push(component);
        component.gameObject.SetActive(false);
    }
}