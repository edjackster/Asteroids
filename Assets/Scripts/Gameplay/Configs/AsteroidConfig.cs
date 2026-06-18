using System;

[Serializable]
public class AsteroidConfig: IConfig
{
    public readonly PhysicsConfig PhysicsConfig = new();
    public readonly float MinSpeed = .5f;
    public readonly float MaxSpeed = 1f;
    public readonly float MinRotationSpeed = 30f;
    public readonly float MaxRotationSpeed = 60f;
    public readonly int MinPartsCount = 1;
    public readonly int MaxPartsCount = 3;
}