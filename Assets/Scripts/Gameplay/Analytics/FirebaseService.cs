using System;
using Core.StateMachine;
using Zenject;

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
        _signalBus.Subscribe<EnterStateSignal<GameState>>(ShowGameOverAd);
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<EnterStateSignal<GameState>>(ShowGameOverAd);
    }

    private void ShowGameOverAd(EnterStateSignal<GameState> signal)
    {
        if (signal.State is not GameOverState)
            return;
        
        _firebaseProvider.LogDeathEvent(_scoreCounter.Score);
    }
}