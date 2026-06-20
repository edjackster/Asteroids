using MVVM;
using UnityEngine;

namespace UI.View
{
    public class GameOverScreenView : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _mobileInputUI;
    
        [SerializeField]
        private RectTransform _gameOverScreen;

        [Setter("GameOverScreen")]
        public bool GameOverScreen
        {
            set
            {
                _gameOverScreen.gameObject.SetActive(value);
            
                if(Application.platform == RuntimePlatform.Android)
                    _mobileInputUI.gameObject.SetActive(!value);
            }
        }
    }
}