namespace Core.StateMachine
{
    public class StateMachine<T> where T:IState
    {
        public T CurrentState { get; private set; }

        public void ChangeState(T newState)
        {
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState?.Enter();
        }
    }
}