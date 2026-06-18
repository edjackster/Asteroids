using System;
using UnityEngine;

namespace Core.Input
{
    public interface IInput
    {    
        public event Action<Vector2> Moved;
        public event Action<bool> MainFire;
        public event Action SecondaryFire;
    }
}
