using System;
using Gameplay.Enemies;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    private EnemySpawnerConfig _config;
    private float _minX, _maxX, _minY, _maxY;
    private EnemyPoolFacade _pool;
    private SignalBus _signalBus;
    private Timer _timer;
    private int _enemiesCount;

    [Inject]
    public void Construct(EnemyPoolFacade pool, Timer timer, SignalBus signalBus, EnemySpawnerConfig config)
    {
        _pool = pool;
        _timer = timer;
        _signalBus = signalBus;
        _config = config;
    }

    private void Start()
    {
        Camera cam = Camera.main;

        if (cam is null)
            return;

        Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));

        _minX = bottomLeft.x;
        _minY = bottomLeft.y;
        _maxX = topRight.x;
        _maxY = topRight.y;
    }

    private void OnEnable()
    {
        _timer.Completed += SpawnEnemy;
        _signalBus.Subscribe<DespawnSignal<Enemy>>(DespawnEnemy);
    }

    private void OnDisable()
    {
        _timer.Completed -= SpawnEnemy;
        _signalBus.Unsubscribe<DespawnSignal<Enemy>>(DespawnEnemy);
    }

    private void Update()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        if (_enemiesCount >= _config.MaxEnemyCount)
            return;

        if (_timer.IsRunning)
            return;

        _timer.Start(_config.SpawnDelay);

        EnemyType type = GetEnemyType();

        Enemy enemy = _pool.Get(type);
        enemy.transform.position = GetSpawnPosition();
        _enemiesCount++;
    }

    private EnemyType GetEnemyType()
    {
        var chance = Random.value;

        if (chance <= _config.UfoSpawnChance)
            return EnemyType.Ufo;

        return EnemyType.Asteroid;
    }

    private Vector3 GetSpawnPosition()
    {
        var sidesCount = Enum.GetNames(typeof(ScreenSide)).Length;
        ScreenSide side = (ScreenSide)Random.Range(0, sidesCount);

        switch (side)
        {
            case ScreenSide.Left:
                return new Vector3(_minX - _config.SpawnOffset, Random.Range(_minY, _maxY), 0);

            case ScreenSide.Right:
                return new Vector3(_maxX + _config.SpawnOffset, Random.Range(_minY, _maxY), 0);

            case ScreenSide.Top:
                return new Vector3(Random.Range(_minX, _maxX), _maxY + _config.SpawnOffset, 0);

            case ScreenSide.Bottom:
                return new Vector3(Random.Range(_minX, _maxX), _minY - _config.SpawnOffset, 0);
        }

        throw new Exception("Invalid ScreenSide");
    }

    private void DespawnEnemy(DespawnSignal<Enemy> signal)
    {
        var enemy = signal.Item;

        _pool.Release(enemy);
        _enemiesCount--;

        if (enemy is Asteroid asteroid)
            SpawnAsteroidParts(asteroid);
    }

    private void SpawnAsteroidParts(Asteroid asteroid)
    {
        Vector3[] directions = RotationTool.GetSplitDirections(asteroid.transform.up, asteroid.PartsCount, 360f);

        _enemiesCount += asteroid.PartsCount;

        for (int i = 0; i < asteroid.PartsCount; i++)
        {
            var part = _pool.Get(EnemyType.AsteroidPart) as AsteroidPart;
            part.transform.position = asteroid.transform.position + directions[i] * .5f;
            part.SetDirection(directions[i]);
        }
    }
}