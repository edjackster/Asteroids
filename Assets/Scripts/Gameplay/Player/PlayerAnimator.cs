using UnityEngine;

namespace Gameplay.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimator : MonoBehaviour
    {
        private const string InvincibleState = "InvincibilityFrames";
        private const string DefaultState = "DefaultState";
    
        private Animator _animator;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void PlayInvincibleState()
        {
            _animator.Play(InvincibleState);
        }

        public void PlayDefaultState()
        {
            _animator.Play(DefaultState);
        }
    }
}
