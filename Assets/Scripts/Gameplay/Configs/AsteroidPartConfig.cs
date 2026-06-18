using System;

[Serializable]
public class AsteroidPartConfig: IConfig
{
    public readonly PhysicsConfig PhysicsConfig = new();
    public readonly float MinSpeed = 1f;
    public readonly float MaxSpeed = 2f;
    public readonly float MinRotationSpeed = 60f;
    public readonly float MaxRotationSpeed = 90f;
}