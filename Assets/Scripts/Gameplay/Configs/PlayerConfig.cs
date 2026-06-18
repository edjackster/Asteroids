using System;

[Serializable]
public class PlayerConfig: IConfig
{
    public readonly PhysicsConfig PhysicsConfig = new();
    public readonly HealthConfig Health = new();
    public readonly float KnockbackDuration = 3;
    public readonly float KnockbackGravityScale = .15f;
    public readonly GunConfig Gun = new();
    public readonly LaserAmmunitionConfig LaserAmmunition = new();
    public readonly LaserConfig Laser = new();
}