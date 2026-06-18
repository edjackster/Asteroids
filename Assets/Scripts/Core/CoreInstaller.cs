using Core.StateMachine;
using Zenject;

public class CoreInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<StateMachine>().AsTransient();
    }
}