using Zenject;

public class ViewModelsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
            .BindInterfacesAndSelfTo<HealthViewModel>()
            .AsSingle()
            .NonLazy();
        
        Container
            .BindInterfacesAndSelfTo<LaserAmmoViewModel>()
            .AsSingle()
            .NonLazy();
        
        Container
            .BindInterfacesAndSelfTo<ScoreViewModel>()
            .AsSingle()
            .NonLazy();
        
        Container
            .BindInterfacesAndSelfTo<PlayerParametersViewModel>()
            .AsSingle()
            .NonLazy();
        
        Container
            .BindInterfacesAndSelfTo<GameOverViewModel>()
            .AsSingle()
            .NonLazy();
    }
}