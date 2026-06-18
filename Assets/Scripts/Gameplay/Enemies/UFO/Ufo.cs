using Gameplay.Enemies;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(PhysicsBody2D))]
public class Ufo : Enemy
{
    private UfoConfig _config;
    private Transform _target;
    
    public override EnemyType Type => EnemyType.Ufo;

    [Inject]
    public void Construct(Player player,  UfoConfig config)
    {
        _target = player.transform;
        _config = config;
    }

    private void Update()
    {
        var direction = (_target.position - transform.position).normalized;
        direction *= _config.Speed;
        
        PhysicsBody.SetDesiredDirection(direction);
        PhysicsBody.SetDesiredVelocity(direction);
    }

    public override void OnSpawned()
    {
    }
}