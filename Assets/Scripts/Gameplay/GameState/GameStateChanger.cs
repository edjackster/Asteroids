using System;
using Core.Signals;
using Core.StateMachine;
using Gameplay.GameState.States;
using Gameplay.Signals;
using Zenject;

namespace Gameplay.GameState
{
    public class GameStateChanger: IInitializable, IDisposable
    {
        private SignalBus _signalBus;
        private StateMachine<States.GameState> _stateMachine;
        
        private PlayingState _playingState;
        private ShowAdState _showAdState;
        private GameOverState _gameOverState;

        public GameStateChanger(SignalBus signalBus, StateMachine<States.GameState> stateMachine, PlayingState  playingState, ShowAdState showAdState, GameOverState gameOverState)
        {
            _signalBus = signalBus;
            _stateMachine = stateMachine;
            _playingState = playingState;
            _showAdState = showAdState;
            _gameOverState = gameOverState;
        }

        public void Initialize()
        {
            _stateMachine.ChangeState(_playingState);
            
            _signalBus.Subscribe<PlayerDiedSignal>(ShowAd);
            _signalBus.Subscribe<AdEndSignal>(Lose);
            _signalBus.Subscribe<AdFailedSignal>(Lose);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<PlayerDiedSignal>(ShowAd);
            _signalBus.Unsubscribe<AdEndSignal>(Lose);
            _signalBus.Unsubscribe<AdFailedSignal>(Lose);
        }

        private void Lose()
        {
            _stateMachine.ChangeState(_gameOverState);
        }

        private void ShowAd()
        {
            _stateMachine.ChangeState(_showAdState);
        }
    }
}