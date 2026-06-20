using Core.Advertisement;
using Zenject;

namespace Gameplay.GameState.States
{
    public class ShowAdState : GameState
    {
        private readonly AdMobProvider _adMobProvider;
        
        public ShowAdState(SignalBus signalBus, AdMobProvider adMobProvider) : base(signalBus)
        {
            _adMobProvider = adMobProvider;
        }

        public override void Enter()
        {
            base.Enter();
            _adMobProvider.ShowInterstitial();
        }
    }
}