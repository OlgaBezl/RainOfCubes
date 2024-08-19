using UnityEngine;

public abstract class GenericGenerator<T> : MonoBehaviour where T : MonoBehaviour, IPoolItem<T>
{
    [SerializeField] private GenericPool<T> _pool;

    public bool IsActive { get; private set; }

    protected virtual void Start()
    {
        IsActive = true;
    }

    protected virtual void Reset()
    {
        IsActive = false;
        _pool.Reset();
    }

    protected virtual T Spawn()
    {
        return _pool.GetObject();
    }
}
