using UnityEngine;
using Zenject;

namespace Gameplay.GameState.States
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