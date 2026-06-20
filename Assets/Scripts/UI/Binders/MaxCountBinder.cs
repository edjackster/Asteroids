using System;
using MVVM;

namespace UI.Binders
{
    public class MaxCountBinder : IBinder
    {
        private readonly Action<int> _viewSetMaxCount;
        private readonly int _viewModelMaxCount;

        public MaxCountBinder(Action<int> viewSetMaxCount, int viewModelMaxCount)
        {
            _viewSetMaxCount = viewSetMaxCount;
            _viewModelMaxCount = viewModelMaxCount;
        }

        public void Bind()
        {
            _viewSetMaxCount?.Invoke(_viewModelMaxCount);
        }

        public void Unbind()
        {
        }
    }
}