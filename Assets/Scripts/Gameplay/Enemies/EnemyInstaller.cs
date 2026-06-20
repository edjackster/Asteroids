using Core.Signals;
using Core.Spawns;
using Gameplay.Enemies.Asteroid;
using Gameplay.Enemies.Spawn;
using Gameplay.Enemies.UFO;
using UnityEngine;
using Zenject;

namespace Gameplay.Enemies
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private Asteroid.Asteroid _asteroidPrefab;
        [SerializeField] private AsteroidPart _asteroidPartPrefab;
        [SerializeField] private Ufo _ufoPrefab;

        [SerializeField] private Transform _enemyParent;
        [SerializeField] private Transform _spawnPosition;

        public override void InstallBindings()
        {
            BindEnemyPools();
            DeclareDespawnSignals();
            BindServices();
        }

        private void BindServices()
        {
            Container
                .BindInterfacesAndSelfTo<EnemySpawnerService>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<AsteroidDestructionService>()
                .AsSingle()
                .NonLazy();
        }

        private void DeclareDespawnSignals()
        {
            Container.DeclareSignal<DespawnSignal<Enemy>>();
        }

        private void BindEnemyPools()
        {
            BindPoolTool.Bind<Asteroid.Asteroid>(Container, _asteroidPrefab, _spawnPosition, _enemyParent);
            BindPoolTool.Bind<AsteroidPart>(Container,_asteroidPartPrefab, _spawnPosition, _enemyParent);
            BindPoolTool.Bind<Ufo>(Container,_ufoPrefab, _spawnPosition, _enemyParent);

            Container.Bind<EnemyPoolFacade>().AsSingle();
        }
    }
}