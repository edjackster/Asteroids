using Core.Signals;
using Core.StateMachine;
using Zenject;

namespace Gameplay.GameState.States
{
    public abstract class GameState: IState
    {
        protected SignalBus SignalBus;

        protected GameState(SignalBus signalBus)
        {
            SignalBus = signalBus;
        }
        
        public virtual void Enter()
        {
            SignalBus.Fire( new EnterStateSignal<GameState>(this));
        }

        public virtual void Exit()
        {
            SignalBus.Fire( new ExitStateSignal<GameState>(this));
        }
    }
}