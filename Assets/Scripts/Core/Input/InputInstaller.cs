using UnityEngine;
using Zenject;

namespace Core.Input
{
    public class InputInstaller : MonoInstaller
    {
        [SerializeField] private Canvas _mobileInterface;
        [SerializeField] private JoystickHandler _joystick;
        [SerializeField] private MobileButton _fireButton;
        [SerializeField] private MobileButton _laserButton;
    
        public override void InstallBindings()
        {
            switch (Application.platform)
            {
                case RuntimePlatform.WindowsEditor:
                case RuntimePlatform.WindowsPlayer:
                    Container
                        .BindInterfacesTo<DesktopInput>()
                        .AsSingle();
                    break;
            
                case RuntimePlatform.Android:
                    _mobileInterface.gameObject.SetActive(true);

                    Container
                        .BindInterfacesTo<MobileInput>()
                        .AsSingle()
                        .WithArguments(_joystick, _fireButton, _laserButton);
                    break;
            }
        }
    }
}