using System;
using Core.StateMachine;
using Zenject;

namespace Gameplay.GameState
{
    public class GameStateChanger: IInitializable, IDisposable
    {
        private SignalBus _signalBus;
        private StateMachine _stateMachine;
        
        private PlayingState _playingState;
        private ShowAdState _showAdState;
        private GameOverState _gameOverState;

        public GameStateChanger(SignalBus signalBus, StateMachine stateMachine, PlayingState  playingState, ShowAdState showAdState, GameOverState gameOverState)
        {
            _signalBus = signalBus;
            _stateMachine = stateMachine;
            _playingState = playingState;
            _showAdState = showAdState;
            _gameOverState = gameOverState;
            
            _stateMachine.ChangeState(_playingState);
        }

        public void Initialize()
        {
            _signalBus.Subscribe<PlayerDiedSignal>(ShowAd);
            _signalBus.Subscribe<AdEndSignal>(Lose);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<PlayerDiedSignal>(ShowAd);
            _signalBus.Unsubscribe<AdEndSignal>(Lose);
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