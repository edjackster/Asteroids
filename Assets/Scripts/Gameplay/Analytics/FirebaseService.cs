using System;
using Core.Firebase;
using Core.Signals;
using Gameplay.GameState.States;
using Gameplay.Player;
using Zenject;

namespace Gameplay.Analytics
{
    public class FirebaseService : IInitializable, IDisposable
    {
        private readonly SignalBus _signalBus;
        private readonly FirebaseProvider _firebaseProvider;
        private readonly ScoreCounter _scoreCounter;

        public FirebaseService(SignalBus signalBus, FirebaseProvider firebaseProvider, ScoreCounter scoreCounter)
        {
            _signalBus = signalBus;
            _firebaseProvider = firebaseProvider;
            _scoreCounter = scoreCounter;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<EnterStateSignal<GameState.States.GameState>>(LogGameOver);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<EnterStateSignal<GameState.States.GameState>>(LogGameOver);
        }

        private void LogGameOver(EnterStateSignal<GameState.States.GameState> signal)
        {
            if (signal.State is not GameOverState)
                return;
        
            _firebaseProvider.LogDeathEvent(_scoreCounter.Score);
        }
    }
}