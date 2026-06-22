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
        
            Container.Bind<HealthConfig>().FromInstance(player.HealthConfig).AsSingle();
            Container.Bind<LaserAmmunitionConfig>().FromInstance(player.LaserAmmunitionConfig).AsSingle();
            Container.Bind<LaserConfig>().FromInstance(player.LaserConfig).AsSingle();
            Container.Bind<GunConfig>().FromInstance(player.GunConfig).AsSingle();
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