using System;
using Tools.Runtime;
using UnityEngine;
using Zenject;

namespace Gameplay.GameZone
{
    public class ScreenWrapper : MonoBehaviour
    {
        private const float OffsetDelta = .99f;
    
        [SerializeField] private float _wrapOffset = 0.5f;
    
        private ScreenEdgeTool _screenEdgeTool;
    
        public event Action Wrapped;

        [Inject]
        public void Construct(ScreenEdgeTool tool)
        {
            _screenEdgeTool = tool;
        }

        private void FixedUpdate()
        {
            WrapPosition();
        }

        private void WrapPosition()
        {
            Vector2 pos = transform.position;
            bool wrapped = false;
            float newOffset = _wrapOffset * OffsetDelta;

            if (pos.x < _screenEdgeTool.MinX - _wrapOffset)
            {
                pos.x = _screenEdgeTool.MaxX + newOffset;
                wrapped = true;
            }
            else if (pos.x > _screenEdgeTool.MaxX + _wrapOffset)
            {
                pos.x = _screenEdgeTool.MinX - newOffset;
                wrapped = true;
            }

            if (pos.y < _screenEdgeTool.MinY - _wrapOffset)
            {
                pos.y = _screenEdgeTool.MaxY + newOffset;
                wrapped = true;
            }
            else if (pos.y > _screenEdgeTool.MaxY + _wrapOffset)
            {
                pos.y = _screenEdgeTool.MinY - newOffset;
                wrapped = true;
            }

            if(wrapped)
            {
                transform.position = pos;
                Wrapped?.Invoke();
            }
        }
    }
}