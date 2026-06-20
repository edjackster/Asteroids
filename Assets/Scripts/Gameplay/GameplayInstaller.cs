using Core.Signals;
using Gameplay.Analytics;
using Gameplay.Configs;
using Tools.Runtime.Json;
using Zenject;

namespace Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindConfigs();
            DeclareEventSignals();
        
            Container
                .BindInterfacesAndSelfTo<FirebaseService>()
                .AsSingle()
                .NonLazy();
        }

        private void BindConfigs()
        {
            BindConfig<BulletConfig>();
            BindConfig<AsteroidPartConfig>();
            BindConfig<AsteroidConfig>();
            BindConfig<UfoConfig>();
            BindConfig<EnemySpawnerConfig>();
            BindConfig<ScoreConfig>();
            BindConfig<PlayerConfig>();
        
            var player = Container.Resolve<PlayerConfig>();
        
            Container.Bind<HealthConfig>().FromInstance(player.Health).AsSingle();
            Container.Bind<LaserAmmunitionConfig>().FromInstance(player.LaserAmmunition).AsSingle();
            Container.Bind<LaserConfig>().FromInstance(player.Laser).AsSingle();
            Container.Bind<GunConfig>().FromInstance(player.Gun).AsSingle();
        }

        private void BindConfig<T>()
        {
            Container
                .Bind<T>()
                .FromInstance(JsonConverter.Load<T>())
                .AsSingle();
        }

        private void DeclareEventSignals()
        {
            Container.DeclareSignal<CollisionSignal>();
        }
    }
}