using System;
using UnityEngine;
using Zenject;

namespace Core.Input
{
    public class MobileInput : IInput, ITickable, IDisposable, IInitializable
    {
        private JoystickHandler _joystickHandler;
        private MobileButton _fireButton;
        private MobileButton _laserButton;

        public event Action<Vector2> Moved;
        public event Action<bool> MainFire;
        public event Action SecondaryFire;

        public MobileInput(JoystickHandler joystickHandler, MobileButton fireButton, MobileButton laserButton)
        {
            _joystickHandler = joystickHandler;
            _fireButton = fireButton;
            _laserButton = laserButton;
        }

        public void Initialize()
        {
            _fireButton.ButtonDown += OnMainFireStart;
            _fireButton.ButtonUp += OnMainFireEnd;
            _laserButton.ButtonDown += OnSecondaryFireClick;
        }

        public void Dispose()
        {
            _fireButton.ButtonDown -= OnMainFireStart;
            _fireButton.ButtonUp -= OnMainFireEnd;
            _laserButton.ButtonDown -= OnSecondaryFireClick;
        }

        public void Tick()
        {
            Moved?.Invoke(_joystickHandler.Direction);
        }

        private void OnMainFireStart()
        {
            MainFire?.Invoke(true);
        }

        private void OnMainFireEnd()
        {
            MainFire?.Invoke(false);
        }

        private void OnSecondaryFireClick()
        {
            SecondaryFire?.Invoke();
        }
    }
}