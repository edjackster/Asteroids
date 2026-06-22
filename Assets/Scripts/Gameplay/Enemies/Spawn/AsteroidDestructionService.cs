using System;
using Core.Signals;
using Gameplay.Enemies.Asteroid;
using Tools.Runtime;
using UnityEngine;
using Zenject;

namespace Gameplay.Enemies.Spawn
{
    public class AsteroidDestructionService : IInitializable, IDisposable
    {
        private const float SpawnOffset = 0.5f;
        private const float SpreadAngle = 360f;
        private SignalBus _signalBus;
        private EnemySpawnerService _spawnerService;

        public AsteroidDestructionService(SignalBus signalBus, EnemySpawnerService spawnerService)
        {
            _signalBus = signalBus;
            _spawnerService = spawnerService;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<DespawnSignal<Enemy>>(OnAsteroidDestroy);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<DespawnSignal<Enemy>>(OnAsteroidDestroy);
        }

        private void OnAsteroidDestroy(DespawnSignal<Enemy> signal)
        {
            if (signal.Item is not Asteroid.Asteroid asteroid)
                return;

            Vector3[] directions = RotationTool.GetSplitDirections(asteroid.transform.up, asteroid.PartsCount, SpreadAngle);
            AsteroidPart part;
            Vector3 position = asteroid.transform.position;

            for (int i = 0; i < asteroid.PartsCount; i++)
            {
                part = _spawnerService.SpawnEnemy(EnemyType.AsteroidPart, position + directions[i] * SpawnOffset) as AsteroidPart;
                part?.SetDirection(directions[i]);
            }
        }
    }
}