using Core.Physics;
using Gameplay.Configs;
using Gameplay.Player.Shooting;
using UnityEngine;
using Zenject;

namespace Gameplay.Player
{
    [RequireComponent(typeof(PhysicsBody2D))]
    [RequireComponent(typeof(Movement))]
    [RequireComponent(typeof(PlayerAnimator))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private Gun _gun;
        [SerializeField] private Laser _laser;

        private PhysicsBody2D _physicsBodyBody;
        private Movement _movement;
        private PlayerAnimator _playerAnimator;
        private PlayerConfig _config;

        public PhysicsBody2D PhysicsBody => _physicsBodyBody;
        public Movement Movement => _movement;
        public PlayerAnimator PlayerAnimator => _playerAnimator;
        public Gun Gun => _gun;
        public Laser Laser => _laser;
        
        [Inject]
        public void Construct(PlayerConfig config)
        {
            _config = config;
        }

        private void Awake()
        {
            _physicsBodyBody = GetComponent<PhysicsBody2D>();
            _movement = GetComponent<Movement>();
            _playerAnimator = GetComponent<PlayerAnimator>();
            _physicsBodyBody.Initialize(_config.PhysicsConfig);
        }
    }
}