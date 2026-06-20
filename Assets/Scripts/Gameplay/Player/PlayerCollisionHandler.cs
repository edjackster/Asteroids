using System;
using Core.Signals;
using Gameplay.Enemies;
using Gameplay.Signals;
using UnityEngine;
using Zenject;

namespace Gameplay.Player
{
    public class PlayerCollisionHandler: IInitializable, IDisposable
    {
        private HealthSystem _healthSystem;
        private SignalBus _signalBus;
        private readonly Player _player;
        
        public PlayerCollisionHandler(Player player, SignalBus signalBus)
        {
            _player = player;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _player.PhysicsBody.Collide += HandleCollision;
        }

        public void Dispose()
        {
            _player.PhysicsBody.Collide -= HandleCollision;
        }

        private void HandleCollision(Collider2D otherCollider)
        {
            if (otherCollider.TryGetComponent(out Enemy _) == false)
                return;

            var hitPoint = otherCollider.ClosestPoint(_player.transform.position);
            _signalBus.Fire(new CollisionSignal(hitPoint));
            _signalBus.Fire(new PlayerHitSignal());
        }
    }
}