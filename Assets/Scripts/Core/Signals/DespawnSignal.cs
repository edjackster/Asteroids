using UnityEngine;

namespace Core.Signals
{
    public struct DespawnSignal<T> where T : Component
    {
        public T Item;

        public DespawnSignal(T item)
        {
            Item = item;
        }
    }
}