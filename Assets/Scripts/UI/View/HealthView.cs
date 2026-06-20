using System.Collections.Generic;
using MVVM;
using UnityEngine;
using UnityEngine.UI;

namespace UI.View
{
    public class HealthView : MonoBehaviour
    {
        [SerializeField] private Image _heartImage;

        private readonly List<Image> _hearts = new();

        [Method("MaxHealth")]
        public void SetMaxHealth(int value)
        {
            for (int i = 0; i < value; i++)
            {
                _hearts.Add(Instantiate(_heartImage, transform));
            }
        }

        [Method("Health")]
        public void OnHealthChanged(int value)
        {
            for (int i = 0; i < _hearts.Count; i++)
            {
                _hearts[i].enabled = i < value;
            }
        }
    }
}