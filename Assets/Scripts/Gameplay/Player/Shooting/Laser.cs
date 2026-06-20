using Core.Input;
using Gameplay.Configs;
using Gameplay.Enemies;
using Tools.Runtime;
using UnityEngine;
using Zenject;

namespace Gameplay.Player.Shooting
{
    public class Laser : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Collider2D _collider2D;

        private IInput _input;
        private bool _canShoot = true;
        private Timer _timer = new();
        private LaserAmmunition _ammunition;
        private LaserConfig _config;

        [Inject]
        public void Construct(IInput input, LaserAmmunition ammunition, LaserConfig config)
        {
            _ammunition = ammunition;
            _input = input;
            _config = config;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.TryGetComponent(out Enemy enemy) == false)
                return;
        
            enemy.Hit();
        }

        private void OnEnable()
        {
            _input.SecondaryFire += TryStartLaser;
        }

        private void OnDisable()
        {
            _input.SecondaryFire -= TryStartLaser;
            _timer.Completed -= StopLaser;
            _timer.Completed -= RecoverLaser;
            _timer.Cancel();
            SetComponentsActive(false);
            _canShoot = true;
        }

        private void TryStartLaser()
        {
            if (_canShoot == false)
                return;
        
            if (_ammunition.CurrentAmount <= 0)
                return;
        
            _ammunition.SpendAmmo();
            SetComponentsActive(true);
            _canShoot = false;
        
            _timer.Completed += StopLaser;
            _timer.Start(_config.Duration);
        }

        private void StopLaser()
        {
            _timer.Completed -= StopLaser;

            SetComponentsActive(false);
        
            _timer.Completed += RecoverLaser;
            _timer.Start(_config.Cooldown);
        }

        private void RecoverLaser()
        {
            _timer.Completed -= RecoverLaser;
            _canShoot = true;
        }

        private void SetComponentsActive(bool active)
        {
            _collider2D.enabled = active;
            _lineRenderer.enabled = active;
        }
    }
}