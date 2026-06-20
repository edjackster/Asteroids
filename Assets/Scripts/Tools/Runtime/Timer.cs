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
        public event Action Canceled;
        public event Action<float> CountDown;

        public bool IsRunning { get; private set; }
        public float RemainingTime { get; private set; }

        public void Start(float duration)
        {
            Cancel();

            _cancellationTokenSource = new CancellationTokenSource();
            Run(duration, _cancellationTokenSource.Token).Forget(OnError);
        }

        public void Cancel()
        {
            IsRunning = false;
            _cancellationTokenSource?.Cancel();
            ClearTimerState();
        }

        private async UniTask Run(float duration, CancellationToken token)
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
                Canceled?.Invoke();
            }
            finally
            {
                ClearTimerState();
            }
        
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
            Canceled?.Invoke();
        }
    }
}
