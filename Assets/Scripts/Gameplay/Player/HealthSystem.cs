using System;
using Gameplay.Configs;
using Gameplay.Signals;
using Zenject;

namespace Gameplay.Player
{
    public class HealthSystem: IInitializable, IDisposable
    {
        private const int MinHealth = 0;
        private const int DamageAmount = 1;
    
        private HealthConfig _config;
    
        private int _currentHealth;
        private SignalBus _signalBus;

        public int MaxHealth => _config.MaxHealth;
        public bool IsDead => _currentHealth <= 0;

        public int CurrentHealth
        {
            get => _currentHealth;

            private set
            {
                _currentHealth = value;
            
                if(CurrentHealth  == 0)
                    _signalBus.Fire<PlayerDiedSignal>();
            
                HealthChanged?.Invoke(value);
            }
        }

        public event Action<int> HealthChanged;

        public HealthSystem(SignalBus signalBus,HealthConfig config)
        {
            CurrentHealth = config.MaxHealth;
            _config = config;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<PlayerHitSignal>(TakeDamage);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<PlayerHitSignal>(TakeDamage);
        }

        public void TakeDamage()
        {
            if(IsDead)
                return;

            CurrentHealth = Math.Clamp(CurrentHealth - DamageAmount, MinHealth, _config.MaxHealth);
        }
    }
}