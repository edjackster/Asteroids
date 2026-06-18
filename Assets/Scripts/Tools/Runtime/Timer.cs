using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Timer
{
    private CancellationTokenSource _cancellationTokenSource;

    public event Action Completed;
    // public event Action Canceled;
    public event Action<float> CountDown;

    public bool IsRunning { get; private set; }
    public float RemainingTime { get; private set; }

    public void Start(float duration)
    {
        Cancel();

        _cancellationTokenSource = new CancellationTokenSource();
        Run(duration, _cancellationTokenSource.Token).Forget();
    }

    public void Cancel()
    {
        if (_cancellationTokenSource == null) return;
        
        IsRunning = false;
        _cancellationTokenSource.Cancel();
        _cancellationTokenSource.Dispose();
        _cancellationTokenSource = null;
    }

    private async UniTaskVoid Run(float duration, CancellationToken token)
    {
        IsRunning = true;
        RemainingTime = duration;

        try
        {
            while (RemainingTime > 0f)
            {
                await UniTask.Yield(PlayerLoopTiming.Update, token);

                RemainingTime -= Time.deltaTime;

                if (RemainingTime < 0f)
                    RemainingTime = 0f;

                CountDown?.Invoke(RemainingTime);
            }
            
            IsRunning = false;
            Completed?.Invoke();
        }
        catch (OperationCanceledException)
        {
            IsRunning = false;
            // Canceled?.Invoke();
        }
        
    }
}
