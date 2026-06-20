using Gameplay.Configs;

namespace Gameplay.Player.PlayerState
{
    public class ConsciousState: PlayerState
    {
        public ConsciousState(PlayerConfig playerConfig, Player player) : base(playerConfig, player)
        {
        }

        public override void Enter()
        {
            Player.PlayerAnimator.PlayDefaultState();
            Player.PhysicsBody.SetGravity(Config.PhysicsConfig.Gravity);
            Player.PhysicsBody.SetIsColliding(true);
            ChangeInputState(true);
        }
    }
}