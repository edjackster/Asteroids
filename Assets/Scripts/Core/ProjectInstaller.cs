using Core.Advertisement;
using Core.Configs;
using Core.Firebase;
using Core.Signals;
using Tools.Runtime.Json;
using Zenject;

namespace Core
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
        
            BindProviders();
            DeclareSignals();
        
            Container
                .Bind<AdMobConfig>()
                .FromInstance(JsonConverter.Load<AdMobConfig>())
                .AsSingle();
        }

        private void DeclareSignals()
        {
            Container.DeclareSignal<AdEndSignal>();
            Container.DeclareSignal<AdFailedSignal>();
        }

        private void BindProviders()
        {
            Container.Bind<FirebaseProvider>().AsSingle().NonLazy();
            Container.Bind<AdMobProvider>().AsSingle().NonLazy();
        }
    }
}