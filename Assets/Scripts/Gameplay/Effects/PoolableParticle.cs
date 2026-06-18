using UnityEngine;
using Zenject;

[RequireComponent(typeof(ParticleSystem))]
public class PoolableParticle : MonoBehaviour, IPoolable
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

    public void OnParticleOver()
    {
        _signalBus.Fire(new DespawnSignal<PoolableParticle>(this));
    }

    public void OnSpawned()
    {
        _particleSystem.Play();
    }
}