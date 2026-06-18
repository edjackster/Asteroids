using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Core.Input
{
    public class MobileButton: MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
    {
        public event Action ButtonDown;
        public event Action ButtonUp;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            ButtonDown?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ButtonUp?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ButtonUp?.Invoke();
        }
    }
}