using Zenject;

namespace Tools.Runtime
{
    public class ToolInstaller: MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .BindInterfacesAndSelfTo<ScreenEdgeTool>()
                .AsSingle()
                .NonLazy();
        }
    }
}