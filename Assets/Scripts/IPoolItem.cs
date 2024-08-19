using System;

public interface IPoolItem<T>
{
    public event Action<T> Destroyed;

    public void Activate();
}
