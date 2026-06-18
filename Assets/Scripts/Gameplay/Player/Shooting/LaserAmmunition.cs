using System;
using Zenject;

public class LaserAmmunition : IInitializable, IDisposable
{
    private const int AmmoRecoveryCount = 1;
    
    private readonly Timer _timer;
    private int _currentAmount;
    private LaserAmmunitionConfig _config;

    public int MaxAmmo => _config.MaxChargesAmount;
    public float ReloadTime => _config.ReloadTime;

    public int CurrentAmount
    {
        get => _currentAmount;

        private set
        {
            _currentAmount = value;
            AmountChanged?.Invoke(value);
        }
    }

    public event Action<int> AmountChanged;
    public event Action<float> ReloadTimeLeft;

    public LaserAmmunition(Timer timer, LaserAmmunitionConfig config)
    {
        _currentAmount = config.MaxChargesAmount;
        _config = config;
        _timer = timer;
    }

    public void SpendAmmo(int amount = 1)
    {
        if (amount < 0)
            throw new Exception("Amount cannot be negative");

        if (_currentAmount - amount < 0)
            throw new Exception("Spent amount cannot be less than zero");

        CurrentAmount -= amount;

        if (_timer.IsRunning == false)
        {
            _timer.Start(_config.ReloadTime);
        }
    }

    public void Initialize()
    {
        _timer.Completed += AddAmmo;
        _timer.CountDown += OnTimerTick;
    }

    public void Dispose()
    {
        _timer.Completed -= AddAmmo;
        _timer.CountDown -= OnTimerTick;
    }

    private void OnTimerTick(float dt)
    {
        ReloadTimeLeft?.Invoke(dt);
    }

    private void AddAmmo()
    {
        if (CurrentAmount >= _config.MaxChargesAmount)
            return;

        CurrentAmount += AmmoRecoveryCount;

        if (CurrentAmount < _config.MaxChargesAmount)
            _timer.Start(_config.ReloadTime);
    }
}