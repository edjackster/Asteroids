using Core.StateMachine;

namespace Core.Signals
{
    public struct ExitStateSignal<T> where T : IState
    {
        public T State;

        public ExitStateSignal(T state)
        {
            State = state;
        }
    }
}