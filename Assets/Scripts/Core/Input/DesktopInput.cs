using System;
using UnityEngine;
using Zenject;

namespace Core.Input
{
    public class DesktopInput : IInput, ITickable
    {
        private const string HorizontalAxisName = "Horizontal";
        private const string VerticalAxisName = "Vertical";
        
        private Vector2 direction = Vector2.zero;
        
        public event Action<Vector2> Moved;
        public event Action<bool> MainFire;
        public event Action SecondaryFire;

        public void Tick()
        {
            direction.x = UnityEngine.Input.GetAxis(HorizontalAxisName);
            direction.y = UnityEngine.Input.GetAxis(VerticalAxisName);
            
            if (direction != Vector2.zero)
                direction.Normalize();
            
            if (UnityEngine.Input.GetMouseButtonDown(0))
                MainFire?.Invoke(true);
            
            if (UnityEngine.Input.GetMouseButtonUp(0))
                MainFire?.Invoke(false);
            
            if (UnityEngine.Input.GetMouseButtonDown(1))
                SecondaryFire?.Invoke();
            
            Moved?.Invoke(direction);
        }
    }
}