using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    [SerializeField] private Asteroid _asteroidPrefab;
    [SerializeField] private AsteroidPart _asteroidPartPrefab;
    [SerializeField] private Ufo _ufoPrefab;
    
    [SerializeField] private Transform _enemyParent;
    [SerializeField] private Transform _spawnPosition;

    public override void InstallBindings()
    {
        BindEnemyPools();
        DeclareDespawnSignals();
    }

    private void DeclareDespawnSignals()
    {
        Container.DeclareSignal<DespawnSignal<Enemy>>();
    }

    private void BindEnemyPools()
    {
        BindPool<Asteroid>(_asteroidPrefab, _enemyParent);
        BindPool<AsteroidPart>(_asteroidPartPrefab, _enemyParent);
        BindPool<Ufo>(_ufoPrefab, _enemyParent);
        
        Container.Bind<EnemyPoolFacade>().AsSingle();
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