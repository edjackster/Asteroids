using Core.Input;
using Core.Physics;
using UnityEngine;
using Zenject;

namespace Gameplay.Player
{
    [RequireComponent(typeof(PhysicsBody2D))]
    public class Movement : MonoBehaviour
    {
        private PhysicsBody2D _physicsBody;
        private IInput _input;

        [Inject]
        public void Construct(IInput input)
        {
            _input = input;
        }

        private void Start()
        {
            _physicsBody = GetComponent<PhysicsBody2D>();
        }

        private void OnEnable()
        {
            _input.Moved += OnMove;
        }

        private void OnDisable()
        {
            _input.Moved -= OnMove;
        }

        private void OnMove(Vector2 dir)
        {
            _physicsBody.SetDesiredVelocity(transform.up);
            _physicsBody.SetDesiredDirection(dir);
        }
    }
}
