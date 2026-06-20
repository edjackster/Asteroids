using System;
using Core.Signals;
using Core.Spawns;
using UnityEngine;
using Zenject;

namespace Gameplay.Player.Shooting
{
    public class BulletSpawner: IInitializable, IDisposable
    {
        private SignalBus _signalBus;
        private PrefabPool<Bullet> _bulletPool;
    
    
        public BulletSpawner(PrefabPool<Bullet>  bulletPool, SignalBus signalBus)
        {
            _signalBus = signalBus;
            _bulletPool = bulletPool;
        }

    
        public void Initialize()
        {
            _signalBus.Subscribe<DespawnSignal<Bullet>>(Despawn);
        }

        public void Dispose()
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
}
