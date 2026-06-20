using System;
using Core.Physics;
using Tools.Runtime.Json;

namespace Gameplay.Configs
{
    [Serializable]
    public class AsteroidPartConfig: IConfig
    {
        public PhysicsConfig PhysicsConfig = new();
        public float MinSpeed = 1f;
        public float MaxSpeed = 2f;
        public float MinRotationSpeed = 60f;
        public float MaxRotationSpeed = 90f;
    }
}