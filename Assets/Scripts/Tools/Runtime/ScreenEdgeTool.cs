using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Tools.Runtime
{
    public class ScreenEdgeTool : IInitializable
    {
        private float _minX, _maxX, _minY, _maxY;
        
        public float MinX => _minX;
        public float MaxX => _maxX;
        public float MinY => _minY;
        public float MaxY => _maxY;

        public void Initialize()
        {
            Camera cam = Camera.main;

            if (cam is null)
                return;

            Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
            Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));

            _minX = bottomLeft.x;
            _minY = bottomLeft.y;
            _maxX = topRight.x;
            _maxY = topRight.y;
        }

        public Vector3 GetRandomEdgePosition(float offset = 0f)
        {
            var sidesCount = Enum.GetNames(typeof(ScreenSide)).Length;
            ScreenSide side = (ScreenSide)Random.Range(0, sidesCount);

            switch (side)
            {
                case ScreenSide.Left:
                    return new Vector3(_minX - offset, Random.Range(_minY, _maxY), 0);

                case ScreenSide.Right:
                    return new Vector3(_maxX + offset, Random.Range(_minY, _maxY), 0);

                case ScreenSide.Top:
                    return new Vector3(Random.Range(_minX, _maxX), _maxY + offset, 0);

                case ScreenSide.Bottom:
                    return new Vector3(Random.Range(_minX, _maxX), _minY - offset, 0);
            }

            throw new Exception("Invalid ScreenSide");
        }
    }
}