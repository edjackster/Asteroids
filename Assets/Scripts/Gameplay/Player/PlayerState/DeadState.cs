using Gameplay.Configs;

namespace Gameplay.Player.PlayerState
{
    public class DeadState: PlayerState
    {
        public DeadState(PlayerConfig playerConfig, Player player) : base(playerConfig, player)
        {
        }

        public override void Enter()
        {
            ChangeInputState(false);
        }
        
    }
}