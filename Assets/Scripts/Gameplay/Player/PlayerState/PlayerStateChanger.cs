using System;
using Core.StateMachine;
using Gameplay.Configs;
using Tools.Runtime;
using Zenject;

namespace Gameplay.Player.PlayerState
{
    public class PlayerStateChanger : IInitializable, IDisposable
    {
        private readonly StateMachine<PlayerState> _stateMachine = new();
        private readonly Timer _timer = new();
        private readonly PlayerConfig _config;
        private readonly UnconsciousState _unconsciousState;
        private readonly ConsciousState _consciousState;
        private readonly DeadState _deadState;
        private readonly HealthSystem _healthSystem;

        public PlayerStateChanger(PlayerConfig playerConfig, UnconsciousState unconsciousState,
            ConsciousState consciousState, DeadState deadState, HealthSystem healthSystem)
        {
            _config = playerConfig;
            _unconsciousState = unconsciousState;
            _consciousState = consciousState;
            _deadState = deadState;
            _healthSystem = healthSystem;
        }

        public void Initialize()
        {
            _healthSystem.HealthChanged += OnPlayerHit;
            _timer.Completed += EndUnconsciousness;
            
            _stateMachine.ChangeState(_consciousState);
        }

        public void Dispose()
        {
            _healthSystem.HealthChanged -= OnPlayerHit;
            _timer.Completed -= EndUnconsciousness;
        }

        private void OnPlayerHit(int _)
        {
            if (_healthSystem.IsDead)
            {
                _stateMachine.ChangeState(_deadState);
                return;
            }
            
            _timer.Start(_config.KnockbackDuration);
            _stateMachine.ChangeState(_unconsciousState);
        }

        private void EndUnconsciousness()
        {
            _stateMachine.ChangeState(_consciousState);
        }
    }
}