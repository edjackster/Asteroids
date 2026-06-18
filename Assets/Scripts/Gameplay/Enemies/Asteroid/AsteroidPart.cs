using Gameplay.Enemies;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(PhysicsBody2D))]
public class AsteroidPart : Enemy
{
    private AsteroidPartConfig _config;

    public override EnemyType Type => EnemyType.AsteroidPart;
    

    [Inject]
    public void Construct(AsteroidPartConfig config)
    {
        _config = config;
    }
    
    public void SetDirection(Vector3 direction)
    {
        var speed = Random.Range(_config.MinSpeed, _config.MaxSpeed);
        PhysicsBody.SetVelocity(direction * speed);
    }

    public override void OnSpawned()
    {
        var rotationSpeed = Random.Range(_config.MinRotationSpeed, _config.MaxRotationSpeed);
        rotationSpeed *= Mathf.Sign(Random.Range(-1, 1));
        PhysicsBody.SetAngularVelocity(rotationSpeed);
    }
}