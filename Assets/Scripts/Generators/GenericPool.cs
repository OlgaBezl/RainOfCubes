using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericPool<T> : MonoBehaviour where T : MonoBehaviour, IPoolItem<T>
{
    [SerializeField] private Transform _container;
    [SerializeField] private T _prefab;

    private Queue<T> _pool;
    private int _allItemsCount;
    private int _newItemsCount;
    private int _activeItemsCount;

    public event Action<int> AllItemsCountChanged;
    public event Action<int> NewItemsCountChanged;
    public event Action<int> ActiveItemsCountChanged;
    public event Action<Vector3> ItemWasDestroyed;

    private void Awake()
    {
        _pool = new Queue<T>();
    }

    private void OnDisable()
    {
        foreach (T item in _pool)
        {
            item.Destroyed -= ReturnToPool;
            Destroy(item.gameObject);
        }

        _pool.Clear();
    }

    public void Reset()
    {
        OnDisable();
    }

    public virtual T GetObject()
    {
        T newItem;

        if (_pool.Count == 0)
        {
            newItem = Instantiate(_prefab);
            newItem.transform.parent = _container;
            newItem.Destroyed += ReturnToPool;

            _newItemsCount++;
            NewItemsCountChanged?.Invoke(_newItemsCount);
        }
        else
        {
            newItem = _pool.Dequeue();
        }

        newItem.Activate();
                
        _activeItemsCount++;
        ActiveItemsCountChanged?.Invoke(_activeItemsCount);

        _allItemsCount++;
        AllItemsCountChanged?.Invoke(_allItemsCount);

        return newItem;
    }

    public virtual void ReturnToPool(T item)
    {
        item.gameObject.SetActive(false);
        _pool.Enqueue(item);

        _activeItemsCount--;
        ActiveItemsCountChanged?.Invoke(_activeItemsCount);

        ItemWasDestroyed?.Invoke(item.transform.position);
    }
}
