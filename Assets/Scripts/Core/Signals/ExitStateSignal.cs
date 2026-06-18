using Core.StateMachine;

public struct ExitStateSignal<T> where T : IState
{
    public T State;

    public ExitStateSignal(T state)
    {
        State = state;
    }
}