using UnityEngine;
using Zenject;


namespace Core.StateMachine
{
    public class PlayingState : GameState
    {
        public PlayingState(SignalBus signalBus) : base(signalBus)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Time.timeScale = 1;
        }
    }
}