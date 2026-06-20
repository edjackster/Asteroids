using Core.Physics;
using Gameplay.Configs;
using UnityEngine;
using Zenject;

namespace Gameplay.Enemies.Asteroid
{
    [RequireComponent(typeof(PhysicsBody2D))]
    public class AsteroidPart : Enemy
    {
        private AsteroidPartConfig _config;

        public override EnemyType Type => EnemyType.AsteroidPart;


        [Inject]
        public void Construct(AsteroidPartConfig config)
        {
            _config = config;
        }

        protected override void Awake()
        {
            base.Awake();
            PhysicsBody.Initialize(_config.PhysicsConfig);
        }

        public void SetDirection(Vector3 direction)
        {
            var speed = Random.Range(_config.MinSpeed, _config.MaxSpeed);
            PhysicsBody.SetVelocity(direction * speed);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            var rotationSpeed = Random.Range(_config.MinRotationSpeed, _config.MaxRotationSpeed);
            rotationSpeed *= Mathf.Sign(Random.Range(-1, 1));
            PhysicsBody.SetAngularVelocity(rotationSpeed);
        }
    }
}