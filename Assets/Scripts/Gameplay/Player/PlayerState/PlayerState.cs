using Core.StateMachine;
using Gameplay.Configs;
using UnityEngine;
using Zenject;

namespace Gameplay.Player.PlayerState
{
    public abstract class PlayerState : IState
    {
        protected PlayerConfig Config;
        protected Player Player;

        public PlayerState(PlayerConfig playerConfig, Player player)
        {
            Config = playerConfig;
            Player = player;
        }

        public abstract void Enter();

        public virtual void Exit()
        {
        }

        protected void ChangeInputState(bool state)
        {
            Player.Gun.enabled = state;
            Player.Laser.enabled = state;
            Player.Movement.enabled = state;
        }
    }
}