#if UNITY_EDITOR
#endif
using System;
using MVVM;
using UnityEditor;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace UI
{
    public sealed class MonoViewBinder : MonoBehaviour
    {
        private enum BindingMode
        {
            FromInstance = 0,
            FromResolve = 1,
            FromResolveId = 2
        }

        [SerializeField] private BindingMode _viewBinding;

        [SerializeField] private Object _view;

#if UNITY_EDITOR
        [SerializeField] private MonoScript _viewType;
#endif

        [SerializeField] private string _viewId;

        [Space(8)] [SerializeField] private BindingMode _viewModelBinding;

        [SerializeField] private Object _viewModel;

#if UNITY_EDITOR
        [SerializeField] private MonoScript _viewModelType;
#endif

        [SerializeField] private string _viewModelId;

        [Inject] private DiContainer _diContainer;

        private IBinder _binder;

        private void Awake()
        {
            _binder = CreateBinder();
        }

        private void OnEnable()
        {
            _binder.Bind();
        }

        private void OnDisable()
        {
            _binder.Unbind();
        }

        private IBinder CreateBinder()
        {
            object view = _viewBinding switch
            {
                BindingMode.FromInstance => _view,
#if UNITY_EDITOR
                BindingMode.FromResolve => _diContainer.Resolve(_viewType.GetClass()),
                BindingMode.FromResolveId => _diContainer.ResolveId(_viewType.GetClass(), _viewId),
#endif
                _ => throw new Exception($"Binding type of view {_viewBinding} is not found!")
            };

            object model = _viewModelBinding switch
            {
                BindingMode.FromInstance => _viewModel,
#if UNITY_EDITOR
                BindingMode.FromResolve => _diContainer.Resolve(_viewModelType.GetClass()),
                BindingMode.FromResolveId => _diContainer.ResolveId(_viewModelType.GetClass(), _viewModelId),
#endif
                _ => throw new Exception($"Binding type of view {_viewBinding} is not found!")
            };

            return BinderFactory.CreateComposite(view, model);
        }
    }
}