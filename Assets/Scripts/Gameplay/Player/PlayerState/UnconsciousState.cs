using Gameplay.Configs;

namespace Gameplay.Player.PlayerState
{
    public class UnconsciousState: PlayerState
    {
        public UnconsciousState(PlayerConfig playerConfig, Player player) : base(playerConfig, player)
        {
        }

        public override void Enter()
        {
            ChangeInputState(false);
            Player.PhysicsBody.SetIsColliding(false);
            Player.PhysicsBody.SetGravity(Config.KnockbackGravityScale);
            Player.PlayerAnimator.PlayInvincibleState();
        }
    }
}