using System;
using Zenject;

public class HealthSystem
{
    private HealthConfig _config;
    
    private int _currentHealth;
    private SignalBus _signalBus;

    public int MaxHealth => _config.MaxHealth;

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

    public void TakeDamage(int amount = 1)
    {
        if (amount < 0)
            throw new Exception("Damage amount cannot be negative");

        CurrentHealth = Math.Clamp(CurrentHealth - amount, 0, _config.MaxHealth);
    }
}