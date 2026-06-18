using System;

[Serializable]
public class EnemySpawnerConfig: IConfig
{
    public readonly float SpawnOffset = 0.5f;
    public readonly float SpawnDelay = 1.5f;
    public readonly int MaxEnemyCount = 10;
    public readonly float UfoSpawnChance = 0.2f;
}