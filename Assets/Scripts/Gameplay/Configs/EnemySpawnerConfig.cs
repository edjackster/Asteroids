using System;
using Tools.Runtime.Json;

namespace Gameplay.Configs
{
    [Serializable]
    public class EnemySpawnerConfig: IConfig
    {
        public float SpawnOffset = 0.5f;
        public float SpawnDelay = 1.5f;
        public int RateSpawnEnemyLimit = 10;
        public float UfoSpawnChance = 0.2f;
    }
}