using System;
using System.Collections.Generic;
using Gameplay.Enemies;

[Serializable]
public class ScoreConfig: IConfig
{
    public readonly Dictionary<EnemyType, int> Rewards = new(3);
}