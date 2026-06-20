using System;
using Gameplay.Player.Shooting;
using MVVM;
using UniRx;
using Zenject;

namespace UI.ViewModel
{
    public class LaserAmmoViewModel: IDisposable, IInitializable
    {
        private LaserAmmunition _ammunition;

        [Data("Ammo")] 
        public readonly ReactiveProperty<int> Ammo = new();

        [Data("ReloadPercent")] 
        public readonly ReactiveProperty<float> ReloadPercent = new(1);
    
        [Data("MaxAmmo")] 
        public readonly int MaxAmmo;

        public LaserAmmoViewModel(LaserAmmunition ammunition)
        {
            _ammunition = ammunition;
            MaxAmmo = _ammunition.MaxAmmo;
        }

        public void Initialize()
        {
            OnAmmoChanged(_ammunition.CurrentAmount);
            _ammunition.AmountChanged += OnAmmoChanged;
            _ammunition.ReloadTimeLeft += OnReload;
        }

        public void Dispose()
        {
            _ammunition.AmountChanged -= OnAmmoChanged;
            _ammunition.ReloadTimeLeft -= OnReload;
        }

        private void OnAmmoChanged(int health)
        {
            Ammo.Value = health;
        }

        private void OnReload(float dt)
        {
            ReloadPercent.Value = 1f - dt / _ammunition.ReloadTime;
        }
    }
}
