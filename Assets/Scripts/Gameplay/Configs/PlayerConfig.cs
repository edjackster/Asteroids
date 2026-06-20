using System;
using Core.Physics;
using Tools.Runtime.Json;

namespace Gameplay.Configs
{
    [Serializable]
    public class PlayerConfig: IConfig
    {
        public PhysicsConfig PhysicsConfig = new();
        public HealthConfig Health = new();
        public float KnockbackDuration = 3;
        public float KnockbackGravityScale = 0.15f;
        public GunConfig Gun = new();
        public LaserAmmunitionConfig LaserAmmunition = new();
        public LaserConfig Laser = new();
    }
}