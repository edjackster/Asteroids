public struct DespawnSignal<T> where T : IPoolable
{
    public T Item;

    public DespawnSignal(T item)
    {
        Item = item;
    }
}