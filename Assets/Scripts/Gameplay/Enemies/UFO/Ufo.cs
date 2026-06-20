using Core.Physics;
using Gameplay.Configs;
using UnityEngine;
using Zenject;

namespace Gameplay.Enemies.UFO
{
    [RequireComponent(typeof(PhysicsBody2D))]
    public class Ufo : Enemy
    {
        private UfoConfig _config;
        private Transform _target;

        public override EnemyType Type => EnemyType.Ufo;

        [Inject]
        public void Construct(Player.Player player, UfoConfig config)
        {
            _target = player.transform;
            _config = config;
        }

        protected override void Awake()
        {
            base.Awake();
            PhysicsBody.Initialize(_config.PhysicsConfig);
        }

        private void Update()
        {
            var direction = (_target.position - transform.position).normalized;
            direction *= _config.Speed;

            PhysicsBody.SetDesiredDirection(direction);
            PhysicsBody.SetDesiredVelocity(direction);
        }
    }
}