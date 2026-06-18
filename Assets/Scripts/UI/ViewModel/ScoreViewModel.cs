using System;
using MVVM;
using UniRx;
using Zenject;

public class ScoreViewModel: IDisposable, IInitializable
{
    private ScoreCounter _scoreCounter;

    [Data("Score")] 
    public readonly ReactiveProperty<string> Score = new();
    
    public ScoreViewModel(ScoreCounter scoreCounter)
    {
        _scoreCounter = scoreCounter;
    }

    public void Initialize()
    {
        OnScoreChanged(_scoreCounter.Score);
        _scoreCounter.ScoreChanged += OnScoreChanged;
    }

    public void Dispose()
    {
        _scoreCounter.ScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int health)
    {
        Score.Value = health.ToString();
    }
}
