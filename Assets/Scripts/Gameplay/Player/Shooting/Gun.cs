using Core.Input;
using UnityEngine;
using Zenject;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _firePoint;
    [SerializeField] private BulletSpawner _bulletSpawner;

    private IInput _input;
    private bool _isShooting;
    private float _nextFireTime;
    private GunConfig  _config;

    [Inject]
    public void Construct(IInput input, GunConfig config)
    {
        _input = input;
        _config = config;
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
        _input.MainFire -= SetShootingState;
    }

    public void StopShooting()
    {
        _isShooting = false;
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