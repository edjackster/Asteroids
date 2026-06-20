using System;
using Core.Physics;
using Tools.Runtime.Json;

namespace Gameplay.Configs
{
    [Serializable]
    public class UfoConfig: IConfig
    {
        public PhysicsConfig PhysicsConfig = new();
        public float Speed = 0.5f;
    }
}