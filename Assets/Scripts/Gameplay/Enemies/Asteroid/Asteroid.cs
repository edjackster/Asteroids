using Gameplay.Enemies;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(PhysicsBody2D))]
public class Asteroid : Enemy
{
    private int _currentPartsCount;
    private AsteroidConfig _config;

    public int PartsCount => _currentPartsCount;
    public override EnemyType Type => EnemyType.Asteroid;

    [Inject]
    public void Construct(AsteroidConfig config)
    {
        _config = config;
    }

    public override void OnSpawned()
    {
        _currentPartsCount = Random.Range(_config.MinPartsCount, _config.MaxPartsCount);
        
        var speed = Random.Range(_config.MinSpeed, _config.MaxSpeed);
        PhysicsBody.SetVelocity(speed * Random.insideUnitSphere);
        
        var rotationSpeed = Random.Range(_config.MinRotationSpeed, _config.MaxRotationSpeed);
        PhysicsBody.SetAngularVelocity(rotationSpeed);
    }
}