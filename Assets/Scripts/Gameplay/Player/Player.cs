using Core.StateMachine;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(PhysicsBody2D))]
[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(PlayerAnimator))]
public class Player : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private Laser _laser;

    private PhysicsBody2D _physicsBody2D;
    private Movement _movement;
    private PlayerAnimator _playerAnimator;
    private Timer _timer;
    private HealthSystem _healthSystem;
    private SignalBus _signalBus;
    private float _currentGravity;
    private PlayerConfig _config;

    public PhysicsBody2D Physics => _physicsBody2D;

    [Inject]
    public void Construct(Timer timer, HealthSystem healthSystem, SignalBus signalBus, PlayerConfig config)
    {
        _timer = timer;
        _healthSystem = healthSystem;
        _signalBus = signalBus;
        _config = config;
    }

    private void Awake()
    {
        _physicsBody2D = GetComponent<PhysicsBody2D>();
        _movement = GetComponent<Movement>();
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void OnEnable()
    {
        _physicsBody2D.Collide += HandleCollision;
        _signalBus.Subscribe<EnterStateSignal<GameState>>(OnStateChange);
    }

    private void OnDisable()
    {
        _physicsBody2D.Collide -= HandleCollision;
        _signalBus.Unsubscribe<EnterStateSignal<GameState>>(OnStateChange);
    }

    private void OnStateChange(EnterStateSignal<GameState> signal)
    {
        switch (signal.State)
        {
            case GameOverState _:
                DisableInput();
                
                break;

            case PlayingState _:
                EnableInput();
                break;
        }
    }

    private void HandleCollision(Collider2D otherCollider)
    {
        if (otherCollider.TryGetComponent(out Enemy _) == false)
            return;

        var hitPoint = otherCollider.ClosestPoint(transform.position);
        _signalBus.Fire(new CollisionSignal(hitPoint));

        Hit();
    }

    private void Hit()
    {
        DisableInput();

        _currentGravity = _physicsBody2D.GravityScale;

        _physicsBody2D.SetIsColliding(false);
        _physicsBody2D.SetGravity(_config.KnockbackGravityScale);
        _healthSystem.TakeDamage();
        _playerAnimator.PlayInvincibleState();

        _timer.Completed += OnTimerOut;

        _timer.Start(_config.KnockbackDuration);
    }

    private void OnTimerOut()
    {
        _timer.Completed -= OnTimerOut;

        _playerAnimator.PlayDefaultState();

        _physicsBody2D.SetGravity(_currentGravity);
        _physicsBody2D.SetIsColliding(true);

        EnableInput();
    }

    private void DisableInput()
    {
        _gun.StopShooting();
        _gun.enabled = false;
        _laser.enabled = false;
        _movement.enabled = false;
    }

    private void EnableInput()
    {
        _gun.enabled = true;
        _laser.enabled = true;
        _movement.enabled = true;
    }
}