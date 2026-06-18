using System;
using UnityEngine;

[Serializable]
public class PhysicsConfig
{
    public readonly float MaxSpeed;
    public readonly float Acceleration;
    public readonly float Deceleration;

    public readonly float MaxAngularSpeed;
    public readonly float AngularAcceleration;
    public readonly float AngularDeceleration;

    public readonly float MinSpeed;
    public readonly float MinAngularSpeed;

    public readonly LayerMask LayerMask;
    public readonly float Bounciness;
    public readonly float Gravity = 1;
}