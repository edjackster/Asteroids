using System;
using System.Collections.Generic;
using Gameplay.Enemies;
using Tools.Runtime.Json;

namespace Gameplay.Configs
{
    [Serializable]
    public class ScoreConfig: IConfig
    {
        public Dictionary<EnemyType, int> Rewards = new(3);
    }
}