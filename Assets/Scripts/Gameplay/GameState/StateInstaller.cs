using Core.StateMachine;
using Gameplay.GameState;
using Zenject;

public class StateInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        DeclareStateSignals();
        BindStates();
        
        Container
            .BindInterfacesAndSelfTo<GameStateChanger>()
            .AsSingle();
    }

    private void BindStates()
    {
        BindState<PlayingState>();
        BindState<ShowAdState>();
        BindState<GameOverState>();
    }

    private void BindState<T>()
    {
        Container
            .Bind<T>()
            .AsSingle();
    }

    private void DeclareStateSignals()
    {
        Container.DeclareSignal<EnterStateSignal<GameState>>();
        Container.DeclareSignal<ExitStateSignal<GameState>>();
    }
}