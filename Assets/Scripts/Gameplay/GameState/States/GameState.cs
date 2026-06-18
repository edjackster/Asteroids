using Zenject;

namespace Core.StateMachine
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