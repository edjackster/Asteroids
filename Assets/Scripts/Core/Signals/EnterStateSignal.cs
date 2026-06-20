using Core.StateMachine;

namespace Core.Signals
{
    public struct EnterStateSignal<T> where T : IState
    {
        public T State;

        public EnterStateSignal(T state)
        {
            State = state;
        }
    }
}