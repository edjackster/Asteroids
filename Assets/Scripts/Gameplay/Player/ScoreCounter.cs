using System;
using Core.Signals;
using Gameplay.Configs;
using Gameplay.Enemies;
using Zenject;

namespace Gameplay.Player
{
    public class ScoreCounter : IInitializable, IDisposable
    {
        private int _score;
        private SignalBus _signalBus;
        private ScoreConfig _config;

        public int Score
        {
            get => _score;

            private set
            {
                _score = value;
                ScoreChanged?.Invoke(value);
            }
        }

        public event Action<int> ScoreChanged;

        public ScoreCounter(SignalBus signalBus, ScoreConfig scoreConfig)
        {
            _signalBus = signalBus;
            _config = scoreConfig;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<DespawnSignal<Enemy>>(OnEnemyKilled);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<DespawnSignal<Enemy>>(OnEnemyKilled);
        }

        public void AddScore(int amount)
        {
            if (amount < 0)
                throw new ArgumentOutOfRangeException($"{nameof(amount)} cannot be negative");

            Score += amount;
        }

        private void OnEnemyKilled(DespawnSignal<Enemy> signal)
        {
            AddScore(_config.Rewards[signal.Item.Type]);
        }
    }
}