using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        
        Container.Bind<FirebaseProvider>().AsSingle().NonLazy();
        Container.Bind<AdMobProvider>().AsSingle().NonLazy();
        
        Container.DeclareSignal<AdEndSignal>();
    }
}