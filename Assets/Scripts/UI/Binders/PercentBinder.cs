using System;
using MVVM;
using UniRx;

public class PercentBinder : IBinder, IObserver<float>
{
    private readonly Action<float> _viewOnPercentChanged;
    private readonly IReadOnlyReactiveProperty<float> _viewModelPercentProperty;
    private IDisposable _subscription;

    public PercentBinder(Action<float> viewOnPercentChanged, IReadOnlyReactiveProperty<float> viewModelPercentProperty)
    {
        _viewOnPercentChanged = viewOnPercentChanged;
        _viewModelPercentProperty = viewModelPercentProperty;
    }

    public void Bind()
    {
        OnNext(_viewModelPercentProperty.Value);
        _subscription = _viewModelPercentProperty.Subscribe(this);
    }

    public void Unbind()
    {
        _subscription?.Dispose();
        _subscription = null;
    }

    public void OnNext(float value)
    {
        _viewOnPercentChanged?.Invoke(value);
    }

    public void OnCompleted()
    {
    }

    public void OnError(Exception error)
    {
    }
}