using Core.Signals;
using Core.Spawns;
using UnityEngine;
using Zenject;

namespace Gameplay.Effects
{
    public class ParticlesInstaller : MonoInstaller
    {
        [SerializeField] private CollisionEffect _collisionEffectPrefab;
        [SerializeField] private DestroyEffect _destroyEffectPrefab;
        [SerializeField] private Transform _effectsParent;
        [SerializeField] private Transform _spawnPosition;

        public override void InstallBindings()
        {
            BindEffectsPools();
            DeclareSignals();
            BindServices();
        }

        private void BindServices()
        {
            Container
                .BindInterfacesAndSelfTo<EffectsSpawnerService>()
                .AsSingle()
                .NonLazy();
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<DespawnSignal<PoolableParticle>>();
        }

        private void BindEffectsPools()
        {
            BindPoolTool.Bind<CollisionEffect>(Container,_collisionEffectPrefab, _spawnPosition, _effectsParent);
            BindPoolTool.Bind<DestroyEffect>(Container,_destroyEffectPrefab, _spawnPosition, _effectsParent);
        
            Container.Bind<EffectsPoolFacade>().AsSingle();
        }
    }
}