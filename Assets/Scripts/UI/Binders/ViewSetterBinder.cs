using System;
using MVVM;
using UniRx;

public class ViewSetterBinder<T> : IBinder, IObserver<T>
{
    private readonly Action<T> _viewSetter;
    private readonly IReadOnlyReactiveProperty<T> _viewModelProperty;
    private IDisposable _subscription;

    public ViewSetterBinder(Action<T> viewSetter, IReadOnlyReactiveProperty<T> viewModelProperty)
    {
        _viewSetter = viewSetter;
        _viewModelProperty = viewModelProperty;
    }

    public void Bind()
    {
        OnNext(_viewModelProperty.Value);
        _subscription = _viewModelProperty.Subscribe(this);
    }

    public void Unbind()
    {
        _subscription?.Dispose();
        _subscription = null;
    }

    public void OnNext(T value)
    {
        _viewSetter(value);
    }

    public void OnCompleted()
    {
    }

    public void OnError(Exception error)
    {
    }
}