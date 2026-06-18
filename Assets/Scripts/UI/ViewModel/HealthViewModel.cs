using System;
using MVVM;
using UniRx;
using Zenject;

public class HealthViewModel: IDisposable, IInitializable
{
    private HealthSystem _healthSystem;

    [Data("Health")] 
    public readonly ReactiveProperty<int> Health = new();
    
    [Data("MaxHealth")] 
    public readonly int MaxHealth;

    public HealthViewModel(HealthSystem healthSystem)
    {
        _healthSystem = healthSystem;
        MaxHealth = _healthSystem.MaxHealth;
    }

    public void Initialize()
    {
        OnHealthChanged(_healthSystem.CurrentHealth);
        _healthSystem.HealthChanged += OnHealthChanged;
    }

    public void Dispose()
    {
        _healthSystem.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int health)
    {
        Health.Value = health;
    }
}
