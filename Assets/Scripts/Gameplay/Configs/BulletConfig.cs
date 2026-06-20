using System;
using Tools.Runtime.Json;

namespace Gameplay.Configs
{
    [Serializable]
    public class BulletConfig: IConfig
    {
        public float Speed = 7f;
        public float LifeTime = 4f;
    }
}