using System;
using UnityEngine;

public class ScreenWrapper : MonoBehaviour
{
    private const float OffsetDelta = .99f;
    
    [SerializeField] private float _wrapOffset = 0.5f;

    private float _minX, _maxX, _minY, _maxY;
    
    public event Action Wrapped;

    private void Start()
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

    private void FixedUpdate()
    {
        WrapPosition();
    }

    private void WrapPosition()
    {
        Vector2 pos = transform.position;
        bool wrapped = false;
        float newOffset = _wrapOffset * OffsetDelta;

        if (pos.x < _minX - _wrapOffset)
        {
            pos.x = _maxX + newOffset;
            wrapped = true;
        }
        else if (pos.x > _maxX + _wrapOffset)
        {
            pos.x = _minX - newOffset;
            wrapped = true;
        }

        if (pos.y < _minY - _wrapOffset)
        {
            pos.y = _maxY + newOffset;
            wrapped = true;
        }
        else if (pos.y > _maxY + _wrapOffset)
        {
            pos.y = _minY - newOffset;
            wrapped = true;
        }

        if(wrapped)
        {
            transform.position = pos;
            Wrapped?.Invoke();
        }
    }
}