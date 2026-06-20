using Core.Physics;
using Core.Signals;
using Gameplay.Enemies;
using Gameplay.Player.Shooting;
using UnityEngine;
using Zenject;

namespace Gameplay.Enemies
{
    [RequireComponent(typeof(PhysicsBody2D))]
    public abstract class Enemy : MonoBehaviour
    {
        public abstract EnemyType Type { get; }

        protected SignalBus SignalBus;
        protected PhysicsBody2D PhysicsBody;

        [Inject]
        public virtual void Construct(SignalBus signalBus)
        {
            SignalBus = signalBus;
        }

        protected virtual void Awake()
        {
            PhysicsBody = GetComponent<PhysicsBody2D>();
        }

        protected virtual void OnEnable()
        {
            PhysicsBody.Collide += HandleCollision;
        }

        private void OnDisable()
        {
            PhysicsBody.Collide -= HandleCollision;
        }

        public void Hit()
        {
            SignalBus.Fire(new DespawnSignal<Enemy>(this));
        }

        private void HandleCollision(Collider2D collision)
        {
            if (collision.TryGetComponent(out Bullet bullet) == false)
                return;

            Hit();
            bullet.Hit();
        }
    }
}