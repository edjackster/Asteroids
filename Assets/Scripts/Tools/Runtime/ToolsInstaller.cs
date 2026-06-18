using Zenject;

public class ToolsInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<Timer>().AsTransient();
    }
}