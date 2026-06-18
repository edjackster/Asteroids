using UnityEngine;
using Zenject;

public class PlayerInstaller : MonoInstaller
{
    [SerializeField] private Bullet _bulletPrefab;
    
    [SerializeField] private Transform _bulletParent;
    [SerializeField] private Transform _spawnPosition;
    
    [SerializeField] private Player _player;

    public override void InstallBindings()
    {
        BindBulletPool();
        BindPlayer();
        DeclareSignals();
    }

    private void BindPlayer()
    {
        Container
            .Bind<HealthSystem>()
            .AsSingle();

        Container
            .BindInterfacesAndSelfTo<LaserAmmunition>()
            .AsSingle();

        Container
            .BindInterfacesAndSelfTo<ScoreCounter>()
            .AsSingle();
        
        Container
            .BindInstance(_player)
            .AsSingle();
    }

    private void DeclareSignals()
    {
        Container.DeclareSignal<PlayerDiedSignal>();
        Container.DeclareSignal<DespawnSignal<Bullet>>();
    }

    private void BindBulletPool()
    {
        BindPool<Bullet>(_bulletPrefab, _bulletParent);
    }

    private void BindPool<T>(Component prefab, Transform parent = null) where T : Component, IPoolable
    {
        Container
            .Bind<PrefabFactory<T>>()
            .AsSingle()
            .WithArguments(prefab);

        Container
            .Bind<PrefabPool<T>>()
            .AsSingle()
            .WithArguments(_spawnPosition.position, parent);
    }
}