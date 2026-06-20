using System;
using MVVM;
using TMPro;
using UniRx;

namespace UI.Binders
{
    public class TextBinder : IBinder, IObserver<string>
    {
        private readonly TMP_Text _viewTextMesh;
        private readonly IReadOnlyReactiveProperty<string> _viewModelStringProperty;
        private IDisposable _subscription;

        public TextBinder(TMP_Text viewTextMesh, IReadOnlyReactiveProperty<string> viewModelStringProperty)
        {
            _viewTextMesh = viewTextMesh;
            _viewModelStringProperty = viewModelStringProperty;
        }

        public void Bind()
        {
            OnNext(_viewModelStringProperty.Value);
            _subscription = _viewModelStringProperty.Subscribe(this);
        }

        public void Unbind()
        {
            _subscription?.Dispose();
            _subscription = null;
        }

        public void OnNext(string value)
        {
            _viewTextMesh.text = value;
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }
    }
}