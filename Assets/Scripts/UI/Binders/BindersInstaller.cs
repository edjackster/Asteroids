using MVVM;
using Zenject;

public class BindersInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BinderFactory.RegisterBinder<CountBinder>();
        BinderFactory.RegisterBinder<PercentBinder>();
        BinderFactory.RegisterBinder<MaxCountBinder>();
        BinderFactory.RegisterBinder<TextBinder>();
        BinderFactory.RegisterBinder<ViewSetterBinder<bool>>();
    }
}