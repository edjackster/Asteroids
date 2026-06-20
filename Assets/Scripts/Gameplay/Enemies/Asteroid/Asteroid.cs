using Core.Physics;
using Gameplay.Configs;
using UnityEngine;
using Zenject;

namespace Gameplay.Enemies.Asteroid
{
    [RequireComponent(typeof(PhysicsBody2D))]
    public class Asteroid : Enemy
    {
        private const int MaxPartsCountModifier = 1;
        
        private int _currentPartsCount;
        private AsteroidConfig _config;

        public int PartsCount => _currentPartsCount;
        public override EnemyType Type => EnemyType.Asteroid;

        [Inject]
        public void Construct(AsteroidConfig config)
        {
            _config = config;
        }

        protected override void Awake()
        {
            base.Awake();
            PhysicsBody.Initialize(_config.PhysicsConfig);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _currentPartsCount = Random.Range(_config.MinPartsCount, _config.MaxPartsCount + MaxPartsCountModifier);

            var speed = Random.Range(_config.MinSpeed, _config.MaxSpeed);
            PhysicsBody.SetVelocity(speed * Random.insideUnitSphere);

            var rotationSpeed = Random.Range(_config.MinRotationSpeed, _config.MaxRotationSpeed);
            PhysicsBody.SetAngularVelocity(rotationSpeed);
        }
    }
}