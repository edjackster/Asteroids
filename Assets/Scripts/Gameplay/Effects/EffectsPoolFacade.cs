using System;
using Core.Spawns;

namespace Gameplay.Effects
{
    public class EffectsPoolFacade
    {
        private readonly PrefabPool<CollisionEffect> _collisionEffectsPool;
        private readonly PrefabPool<DestroyEffect> _destroyEffectsPool;

        public EffectsPoolFacade(PrefabPool<CollisionEffect> collisionEffectsPool, PrefabPool<DestroyEffect> destroyEffectsPool)
        {
            _collisionEffectsPool = collisionEffectsPool;
            _destroyEffectsPool = destroyEffectsPool;
        }

        public PoolableParticle Get(EffectType type)
        {
            switch (type)
            {
                case EffectType.Collision:
                    return _collisionEffectsPool.Get();
            
                case EffectType.Destroy:
                    return _destroyEffectsPool.Get();
            
                default:
                    throw new ArgumentException($"{type} is not a valid type");
            }
        }

        public void Release(PoolableParticle effect)
        {
            switch (effect)
            {
                case CollisionEffect asteroid:
                    _collisionEffectsPool.Release(asteroid);
                    break;
            
                case DestroyEffect asteroidPart:
                    _destroyEffectsPool.Release(asteroidPart);
                    break;
            }
        }
    }
}