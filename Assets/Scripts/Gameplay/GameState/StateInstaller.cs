using Core.Signals;
using Core.StateMachine;
using Gameplay.GameState.States;
using Zenject;

namespace Gameplay.GameState
{
    public class StateInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            DeclareStateSignals();
            BindStates();
            BindStateMachine();
        }

        private void BindStateMachine()
        {
            Container
                .BindInterfacesAndSelfTo<GameStateChanger>()
                .AsSingle();

            Container
                .Bind<StateMachine<States.GameState>>()
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
            Container.DeclareSignal<EnterStateSignal<States.GameState>>();
            Container.DeclareSignal<ExitStateSignal<States.GameState>>();
        }
    }
}