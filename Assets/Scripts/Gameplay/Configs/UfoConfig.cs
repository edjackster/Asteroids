using System;

[Serializable]
public class UfoConfig: IConfig
{
    public readonly PhysicsConfig PhysicsConfig = new();
    public readonly float Speed = .5f;
}