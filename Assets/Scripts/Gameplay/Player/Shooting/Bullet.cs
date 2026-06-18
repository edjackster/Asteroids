using UnityEngine;
using Zenject;

[RequireComponent(typeof(TrailRenderer))]
public class Bullet : MonoBehaviour, IPoolable
{
    private Vector2 _direction;
    private float _deathTime;
    private SignalBus _signalBus;
    private TrailRenderer _trailRenderer;
    private BulletConfig _config;

    [Inject]
    public void Construct(SignalBus signalBus, BulletConfig bulletConfig)
    {
        _signalBus = signalBus;
        _config = bulletConfig;
    }

    private void Awake()
    {
        _trailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
        if (Time.time >= _deathTime)
        {
            SendDespawnSignal();
        }
    }

    public void OnSpawned()
    {
        _deathTime = Time.time + _config.LifeTime;
    }

    public void SetPositionAndRotation(Vector3 position, Quaternion rotation)
    {
        transform.SetPositionAndRotation(position, rotation);
        _trailRenderer.Clear();
    }

    public void Hit()
    {
        SendDespawnSignal();
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.up * (_config.Speed * Time.fixedDeltaTime));
    }

    private void SendDespawnSignal()
    {
        _signalBus.Fire(new DespawnSignal<Bullet>(this));
    }
}