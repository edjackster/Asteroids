using System;
using UnityEngine;

namespace Core.Physics
{
    [Serializable]
    public class PhysicsConfig
    {
        public float MaxSpeed;
        public float Acceleration;
        public float Deceleration;

        public float MaxAngularSpeed;
        public float AngularAcceleration;
        public float AngularDeceleration;

        public float MinSpeed;
        public float MinAngularSpeed;

        public LayerMask LayerMask;
        public float Bounciness;
        public float Gravity = 1;
    }
}