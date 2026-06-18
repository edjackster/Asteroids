using Core.Input;
using UnityEngine;
using Zenject;

public class Laser : MonoBehaviour
{
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private Collider2D _collider2D;

    private IInput _input;
    private bool _canShoot = true;
    private float _nextFireTime;
    private Timer _timer;
    private LaserAmmunition _ammunition;
    private LaserConfig _config;

    [Inject]
    public void Construct(IInput input, Timer timer, LaserAmmunition ammunition, LaserConfig config)
    {
        _ammunition = ammunition;
        _input = input;
        _timer = timer;
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
    }

    private void TryStartLaser()
    {
        if (_canShoot == false)
            return;
        
        if (_ammunition.CurrentAmount <= 0)
            return;
        
        _ammunition.SpendAmmo();
        
        _lineRenderer.enabled = true;
        _collider2D.enabled = true;
        _canShoot = false;
        
        _timer.Completed += StopLaser;
        _timer.Start(_config.Duration);
    }

    private void StopLaser()
    {
        _timer.Completed -= StopLaser;
        
        _collider2D.enabled = false;
        _lineRenderer.enabled = false;
        
        _timer.Completed += RecoverLaser;
        _timer.Start(_config.Cooldown);
    }

    private void RecoverLaser()
    {
        _timer.Completed -= RecoverLaser;
        _canShoot = true;
    }
}