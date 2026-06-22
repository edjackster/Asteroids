using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Tools.Runtime
{
    public class Timer
    {
        private CancellationTokenSource _cancellationTokenSource;

        public event Action Completed;
        public event Action<float> CountDown;

        public bool IsRunning { get; private set; }
        public float RemainingTime { get; private set; }

        public void Start(float duration)
        {
            Cancel();
            
            var cancellationTokenSource = new CancellationTokenSource();
            _cancellationTokenSource = cancellationTokenSource;
            
            Run(duration, _cancellationTokenSource.Token, cancellationTokenSource).Forget(OnError);
        }

        public void Cancel()
        {
            IsRunning = false;
            _cancellationTokenSource?.Cancel();
            ClearTimerState();
        }

        private async UniTask Run(float duration, CancellationToken token, CancellationTokenSource cancellationTokenSource)
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
            }
            catch (OperationCanceledException)
            {
                return;
            }
            finally
            {
                if (_cancellationTokenSource == cancellationTokenSource)
                    ClearTimerState();
                else
                    cancellationTokenSource.Dispose();
            }
        
            IsRunning = false;
            Completed?.Invoke();
        }

        private void ClearTimerState()
        {
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }

        private void OnError(Exception error)
        {
            Debug.LogError(error);
        }
    }
}
