using Core.StateMachine;

public struct EnterStateSignal<T> where T : IState
{
    public T State;

    public EnterStateSignal(T state)
    {
        State = state;
    }
}