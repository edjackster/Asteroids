using Core.Input;
using Gameplay.Configs;
using UnityEngine;
using Zenject;

namespace Gameplay.Player.Shooting
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
    
        private BulletSpawner _bulletSpawner;
        private IInput _input;
        private bool _isShooting;
        private float _nextFireTime;
        private GunConfig  _config;

        [Inject]
        public void Construct(IInput input, GunConfig config, BulletSpawner bulletSpawner)
        {
            _input = input;
            _config = config;
            _bulletSpawner = bulletSpawner;
        }

        private void Update()
        {
            if (_isShooting && Time.time >= _nextFireTime)
            {
                Shoot();

                _nextFireTime = Time.time + _config.GunCooldown;
            }
        }

        private void OnEnable()
        {
            _input.MainFire += SetShootingState;
        }

        private void OnDisable()
        {
            _isShooting = false;
            _input.MainFire -= SetShootingState;
        }

        private void SetShootingState(bool isShooting)
        {
            _isShooting = isShooting;
        }

        private void Shoot()
        {
            _bulletSpawner.SpawnBullet(_firePoint.position, _firePoint.rotation);
        }
    }
}