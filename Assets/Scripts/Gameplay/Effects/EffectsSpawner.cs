using UnityEngine;
using Zenject;

public class EffectsSpawner : MonoBehaviour
{
    private EffectsPoolFacade _pool;
    private SignalBus _signalBus;

    [Inject]
    public void Construct(EffectsPoolFacade pool, SignalBus signalBus)
    {
        _pool = pool;
        _signalBus = signalBus;
    }

    private void OnEnable()
    {
        _signalBus.Subscribe<DespawnSignal<PoolableParticle>>(DespawnEffect);
        _signalBus.Subscribe<DespawnSignal<Enemy>>(SpawnDestroyEffect);
        _signalBus.Subscribe<CollisionSignal>(SpawnCollisionEffect);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<DespawnSignal<PoolableParticle>>(DespawnEffect);
        _signalBus.Unsubscribe<DespawnSignal<Enemy>>(SpawnDestroyEffect);
        _signalBus.Unsubscribe<CollisionSignal>(SpawnCollisionEffect);
    }

    private void SpawnCollisionEffect(CollisionSignal signal)
    {
        PoolableParticle particle = _pool.Get(EffectType.Collision);
        particle.transform.position = signal.Position;
    }

    private void SpawnDestroyEffect(DespawnSignal<Enemy> signal)
    {
        PoolableParticle particle = _pool.Get(EffectType.Destroy);
        particle.transform.position = signal.Item.transform.position;
    }

    private void DespawnEffect(DespawnSignal<PoolableParticle> signal)
    {
        _pool.Release(signal.Item);
    }
}