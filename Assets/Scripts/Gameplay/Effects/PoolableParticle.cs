using Core.Signals;
using UnityEngine;
using Zenject;

namespace Gameplay.Effects
{
    [RequireComponent(typeof(ParticleSystem))]
    public class PoolableParticle : MonoBehaviour
    {
        private SignalBus _signalBus;
        private ParticleSystem _particleSystem;

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        public void OnParticleSystemStopped()
        {
            _signalBus.Fire(new DespawnSignal<PoolableParticle>(this));
        }

        private void OnEnable()
        {
            _particleSystem.Play();
        }
    }
}