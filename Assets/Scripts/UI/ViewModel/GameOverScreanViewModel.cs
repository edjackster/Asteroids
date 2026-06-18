using System;
using Core.StateMachine;
using MVVM;
using UniRx;
using Zenject;

public class GameOverViewModel: IDisposable, IInitializable
{
    private SignalBus _signalBus;
    
    [Data("GameOverScreen")] 
    public readonly ReactiveProperty<bool> IsGameOverScreenOpen = new();

    public GameOverViewModel(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public void Initialize()
    {
        _signalBus.Subscribe<EnterStateSignal<GameState>>(ShowWindow);
        _signalBus.Subscribe<ExitStateSignal<GameState>>(CloseWindow);
    }

    public void Dispose()
    {
        _signalBus.Unsubscribe<EnterStateSignal<GameState>>(ShowWindow);
        _signalBus.Unsubscribe<ExitStateSignal<GameState>>(CloseWindow);
    }

    private void ShowWindow(EnterStateSignal<GameState> signal)
    {
        if(signal.State is not GameOverState)
            return;
        
        IsGameOverScreenOpen.Value = true;
    }

    private void CloseWindow(ExitStateSignal<GameState> signal)
    {
        if(signal.State is not GameOverState)
            return;
        
        IsGameOverScreenOpen.Value = false;
    }
}
