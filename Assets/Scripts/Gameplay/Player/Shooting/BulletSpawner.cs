using UnityEngine;
using Zenject;

public class BulletSpawner : MonoBehaviour
{
    private SignalBus _signalBus;
    private PrefabPool<Bullet> _bulletPool;
    
    [Inject]
    public void Construct(PrefabPool<Bullet>  bulletPool, SignalBus signalBus)
    {
        _signalBus = signalBus;
        _bulletPool = bulletPool;
    }
    
    private void OnEnable()
    {
        _signalBus.Subscribe<DespawnSignal<Bullet>>(Despawn);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<DespawnSignal<Bullet>>(Despawn);
    }

    public void SpawnBullet(Vector3 position, Quaternion rotation)
    {
        var bullet = _bulletPool.Get();
        bullet.SetPositionAndRotation(position, rotation);
    }

    private void Despawn(DespawnSignal<Bullet> signal)
    {
        _bulletPool.Release(signal.Item);
    }
    
}
