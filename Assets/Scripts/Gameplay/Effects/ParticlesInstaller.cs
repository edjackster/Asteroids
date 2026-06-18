using UnityEngine;
using Zenject;

public class ParticlesInstaller : MonoInstaller
{
    [SerializeField] private CollisionEffect _collisionEffectPrefab;
    [SerializeField] private DestroyEffect _destroyEffectPrefab;
    [SerializeField] private Transform _effectsParent;
    [SerializeField] private Transform _spawnPosition;

    public override void InstallBindings()
    {
        BindEffectsPools();
        
        Container.DeclareSignal<DespawnSignal<PoolableParticle>>();
    }

    private void BindEffectsPools()
    {
        BindPool<CollisionEffect>(_collisionEffectPrefab, _effectsParent);
        BindPool<DestroyEffect>(_destroyEffectPrefab, _effectsParent);
        
        Container.Bind<EffectsPoolFacade>().AsSingle();
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