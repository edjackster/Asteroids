using System;

namespace Core.StateMachine
{
    public class StateMachine<T> where T:IState
    {
        public T CurrentState { get; private set; }

        public void ChangeState(T newState)
        {
            if (newState is null)
                throw new ArgumentNullException("State cannot be null");
            
            CurrentState?.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}