using System;
using Gameplay.Enemies;

public class EnemyPoolFacade
{
    private readonly PrefabPool<Asteroid> _asteroidPool;
    private readonly PrefabPool<AsteroidPart> _asteroidPartPool;
    private readonly PrefabPool<Ufo> _ufoPool;

    public EnemyPoolFacade(PrefabPool<Asteroid> asteroidPool, PrefabPool<AsteroidPart> partPool, PrefabPool<Ufo> ufoPool)
    {
        _asteroidPool = asteroidPool;
        _asteroidPartPool = partPool;
        _ufoPool = ufoPool;
    }

    public Enemy Get(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Asteroid:
                return _asteroidPool.Get();
            
            case EnemyType.AsteroidPart:
                return _asteroidPartPool.Get();
            
            case EnemyType.Ufo:
                return _ufoPool.Get();
            
            default:
                throw new ArgumentException($"{type} is not a valid type");
        }
    }

    public void Release(Enemy enemy)
    {
        switch (enemy)
        {
            case Asteroid asteroid:
                _asteroidPool.Release(asteroid);
                break;
            
            case AsteroidPart asteroidPart:
                _asteroidPartPool.Release(asteroidPart);
                break;
            
            case Ufo ufo:
                _ufoPool.Release(ufo);
                break;
        }
    }
}