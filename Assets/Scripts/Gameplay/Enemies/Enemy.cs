using Gameplay.Enemies;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(PhysicsBody2D))]
public abstract class Enemy : MonoBehaviour, IPoolable
{
    public abstract EnemyType Type { get; }
    
    protected SignalBus SignalBus;
    protected PhysicsBody2D PhysicsBody;

    [Inject]
    public virtual void Construct(SignalBus signalBus)
    {
        SignalBus = signalBus;
    }

    private void Awake()
    {
        PhysicsBody = GetComponent<PhysicsBody2D>();
    }

    private void OnEnable()
    {
        PhysicsBody.Collide += HandleCollision;
    }

    private void OnDisable()
    {
        PhysicsBody.Collide -= HandleCollision;
    }

    public void Hit()
    {
        SignalBus.Fire(new DespawnSignal<Enemy>(this));
    }

    private void HandleCollision(Collider2D collision)
    {
        if (collision.TryGetComponent(out Bullet bullet) == false)
            return;

        Hit();
        bullet.Hit();
    }

    public virtual void OnSpawned()
    {
    }
}