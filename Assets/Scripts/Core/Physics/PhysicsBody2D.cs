using System;
using UnityEngine;

namespace Core.Physics
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class PhysicsBody2D : MonoBehaviour
    {
        private const float AvgVelocityModifier = 0.5f;
        private const float MinSlowDownFactor = 10f;
        private const float MaxSlowDownFactor = 35f;
        private const float RotationSpeedModifier = 5;

        [SerializeField] private float _maxSpeed = 5f;
        [SerializeField] private float _acceleration = 10f;
        [SerializeField] private float _deceleration = 12f;

        [SerializeField] private float _maxAngularSpeed = 180f;
        [SerializeField] private float _angularAcceleration = 360f;
        [SerializeField] private float _angularDeceleration = 400f;

        [SerializeField] private float _minSpeed = 1f;
        [SerializeField] private float _minAngularSpeed = 30f;

        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private float _bounciness = 10f;
        [SerializeField] private float _gravity = 1;

        [SerializeField] private Vector2 _velocity;
        [SerializeField] private float _angularVelocity;

        private Vector2 _desiredDirection;
        private Vector2 _desiredVelocity;
        private Collider2D _collider;
        private Rigidbody2D _rigidbody;
        private bool _isColliding = true;

        public Vector2 Velocity => _velocity;
        public Vector2 DesiredDirection => _desiredDirection;
        public float AngularVelocity => _angularVelocity;
        public float GravityScale => _gravity;

        public event Action<Collider2D> Collide;

        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_isColliding == false)
                return;
        
            if (_collider.IsTouching(other) == false)
                return;
        
            if((_layerMask.value & (1 << other.gameObject.layer)) == 0)
                return;
        
            Collide?.Invoke(other);
        
            if(other.TryGetComponent(out PhysicsBody2D otherPhysicsBody) == false)
                return;
        
            if(otherPhysicsBody._isColliding == false)
                return;
        
            var normal = (transform.position - other.transform.position).normalized;

            ResolveCollision(otherPhysicsBody, normal);
            otherPhysicsBody.ResolveCollision(this, -normal);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)_velocity);
        
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position + (Vector3)_desiredVelocity);
        }

        public void Initialize(PhysicsConfig config)
        {
            _maxSpeed = config.MaxSpeed;
            _acceleration = config.Acceleration;
            _deceleration = config.Deceleration;

            _maxAngularSpeed = config.MaxAngularSpeed;
            _angularAcceleration = config.AngularAcceleration;
            _angularDeceleration = config.AngularDeceleration;

            _minSpeed = config.MinSpeed;
            _minAngularSpeed = config.MinAngularSpeed;

            _layerMask = config.LayerMask;
            _bounciness = config.Bounciness;
            _gravity = config.Gravity;
        }

        public void SetDesiredDirection(Vector2 direction)
        {
            _desiredDirection = direction;
        }

        public void SetDesiredVelocity(Vector2 desiredVelocity)
        {
            _desiredVelocity = desiredVelocity;
        }

        public void SetVelocity(Vector2 velocity)
        {
            _velocity = velocity;
        }

        public void SetAngularVelocity(float angularVelocity)
        {
            _angularVelocity = angularVelocity;
        }

        public void SetIsColliding(bool isColliding)
        {
            _isColliding = isColliding;
        }

        public void SetGravity(float gravity)
        {
            _gravity = gravity;
        }

        private void ResolveCollision(PhysicsBody2D other, Vector2 normal)
        {
            Vector2 avgVelocity = (_velocity + other.Velocity) * AvgVelocityModifier;
        
            _velocity = normal * (avgVelocity.magnitude * _bounciness);
            _desiredVelocity = _velocity;
            _desiredDirection = Vector2.zero;

            RotateTowards(normal);
        }

        private void FixedUpdate()
        {
            float dt = Time.fixedDeltaTime;

            HandleRotation(dt);
            HandleMovement(dt);
            Move(dt);
        }

        private void HandleRotation(float dt)
        {
            if (_desiredDirection.sqrMagnitude == 0)
            {
                _angularVelocity = Mathf.MoveTowards(
                    _angularVelocity,
                    0,
                    _angularDeceleration * _gravity * dt
                );
                return;
            }

            Vector2 forward = transform.up;
            float angle = Vector2.SignedAngle(forward, _desiredDirection);
            float slowDownFactor = Mathf.InverseLerp(MinSlowDownFactor, MaxSlowDownFactor, Mathf.Abs(angle));
            float targetAngularVelocity = Mathf.Sign(angle) * _maxAngularSpeed * slowDownFactor;

            if (Mathf.Abs(_angularVelocity) < _minAngularSpeed)
            {
                _angularVelocity = Mathf.Sign(angle) * _minAngularSpeed;
            }

            _angularVelocity = Mathf.MoveTowards(
                _angularVelocity,
                targetAngularVelocity,
                _angularAcceleration * _gravity * dt
            );
        }

        private void HandleMovement(float dt)
        {
            if (_desiredDirection.sqrMagnitude == 0)
            {
                _velocity = Vector2.MoveTowards(
                    _velocity,
                    Vector2.zero,
                    _deceleration * _gravity * dt
                );
            
                return;
            }

            Vector2 targetVelocity = _desiredVelocity * _maxSpeed;

            if (_velocity.magnitude < _minSpeed)
            {
                _velocity = targetVelocity.normalized * _minSpeed;
            }

            _velocity = Vector2.MoveTowards(
                _velocity,
                targetVelocity,
                _acceleration * _gravity * dt
            );
        }

        private void Move(float dt)
        {
            transform.Rotate(Vector3.forward, _angularVelocity * dt);

            Vector2 delta = _velocity * dt;

            if (delta.sqrMagnitude == 0)
                return;

            _rigidbody.MovePosition(transform.position + (Vector3)delta);
        }

        private void RotateTowards(Vector2 direction)
        {
            float angle = Vector2.SignedAngle(transform.up, direction);

            _angularVelocity =
                Mathf.Sign(angle) * Mathf.Min(Mathf.Abs(angle * RotationSpeedModifier), _maxAngularSpeed);
        }
    }
}