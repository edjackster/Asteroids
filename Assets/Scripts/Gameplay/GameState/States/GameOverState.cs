using UnityEngine;
using Zenject;

namespace Core.StateMachine
{
    public class GameOverState : GameState
    {
        public GameOverState(SignalBus signalBus) : base(signalBus)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Time.timeScale = 0;
        }
    }
}