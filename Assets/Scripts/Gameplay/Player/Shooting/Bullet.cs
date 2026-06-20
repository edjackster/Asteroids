using Core.Signals;
using Gameplay.Configs;
using UnityEngine;
using Zenject;

namespace Gameplay.Player.Shooting
{
    [RequireComponent(typeof(TrailRenderer))]
    public class Bullet : MonoBehaviour
    {
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

        private void OnEnable()
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
}