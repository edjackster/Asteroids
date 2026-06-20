using System;
using Core.Physics;
using Tools.Runtime.Json;

namespace Gameplay.Configs
{
    [Serializable]
    public class AsteroidConfig : IConfig
    {
        public PhysicsConfig PhysicsConfig = new();
        public float MinSpeed = 0.5f;
        public float MaxSpeed = 1f;
        public float MinRotationSpeed = 30f;
        public float MaxRotationSpeed = 60f;
        public int MinPartsCount = 1;
        public int MaxPartsCount = 3;
    }
}