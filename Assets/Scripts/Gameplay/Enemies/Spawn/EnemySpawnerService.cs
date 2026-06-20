using System;
using Core.Signals;
using Gameplay.Configs;
using Tools.Runtime;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Gameplay.Enemies.Spawn
{
    public class EnemySpawnerService: IInitializable, IDisposable
    {
        private EnemySpawnerConfig _config;
        private ScreenEdgeTool _screenEdgeTool;
        private EnemyPoolFacade _pool;
        private SignalBus _signalBus;
        private Timer _timer = new();
        private int _enemiesCount;

        public EnemySpawnerService(EnemyPoolFacade pool, SignalBus signalBus, ScreenEdgeTool screenEdgeTool, EnemySpawnerConfig config)
        {
            _pool = pool;
            _signalBus = signalBus;
            _config = config;
            _screenEdgeTool = screenEdgeTool;
        }

        public void Initialize()
        {
            _timer.Completed += DoRateSpawn;
            _signalBus.Subscribe<DespawnSignal<Enemy>>(DespawnEnemy);
        
            DoRateSpawn();
        }

        public void Dispose()
        {
            _timer.Completed -= DoRateSpawn;
            _signalBus.Unsubscribe<DespawnSignal<Enemy>>(DespawnEnemy);
        }

        private void DoRateSpawn()
        {
            SpawnEnemyRandomEnemy();
        
            if (_enemiesCount < _config.RateSpawnEnemyLimit)
                _timer.Start(_config.SpawnDelay);
        }

        private void SpawnEnemyRandomEnemy()
        {
            EnemyType type = GetEnemyType();
            Vector3 position = _screenEdgeTool.GetRandomEdgePosition(_config.SpawnOffset);

            SpawnEnemy(type, position);
        }

        public Enemy SpawnEnemy(EnemyType type, Vector3 position)
        {
            Enemy enemy = _pool.Get(type);
        
            enemy.transform.position = position;
            _enemiesCount++;
        
            return enemy;
        }

        private EnemyType GetEnemyType()
        {
            var chance = Random.value;

            if (chance <= _config.UfoSpawnChance)
                return EnemyType.Ufo;

            return EnemyType.Asteroid;
        }

        private void DespawnEnemy(DespawnSignal<Enemy> signal)
        {
            var enemy = signal.Item;

            _pool.Release(enemy);
            _enemiesCount--;
        
            if(_enemiesCount < _config.RateSpawnEnemyLimit && _timer.IsRunning == false)
                DoRateSpawn();
        }
    }
}