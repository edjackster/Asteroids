using System;
using MVVM;
using UniRx;

public class CountBinder : IBinder, IObserver<int>
{
    private readonly Action<int> _viewOnCountChanged;
    private readonly IReadOnlyReactiveProperty<int> _viewModelCountProperty;
    private IDisposable _subscription;

    public CountBinder(Action<int> viewOnCountChanged, IReadOnlyReactiveProperty<int> viewModelCountProperty)
    {
        _viewOnCountChanged = viewOnCountChanged;
        _viewModelCountProperty = viewModelCountProperty;
    }

    public void Bind()
    {
        OnNext(_viewModelCountProperty.Value);
        _subscription = _viewModelCountProperty.Subscribe(this);
    }

    public void Unbind()
    {
        _subscription?.Dispose();
        _subscription = null;
    }

    public void OnNext(int value)
    {
        _viewOnCountChanged?.Invoke(value);
    }

    public void OnCompleted()
    {
    }

    public void OnError(Exception error)
    {
    }
}