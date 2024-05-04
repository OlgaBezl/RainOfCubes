using System.Collections.Generic;
using UnityEngine;

public class CubePool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private Cube _prefab;

    private Queue<Cube> _pool;

    private void Awake()
    {
        _pool = new Queue<Cube>();
    }

    private void OnDisable()
    {
        foreach (Cube cube in _pool)
        {
            cube.Destroyed -= ReturnToPool;
            Destroy(cube.gameObject);
        }

        _pool.Clear();
    }

    public void Reset()
    {
        OnDisable();
    }

    public Cube GetObject()
    {
        Cube newCube;

        if (_pool.Count == 0)
        {
            newCube = Instantiate(_prefab);
            newCube.transform.parent = _container;
            newCube.Destroyed += ReturnToPool;
        }
        else
        {
            newCube = _pool.Dequeue();
        }

        newCube.Activate();
        return newCube;
    }

    private void ReturnToPool(Cube cube)
    {
        cube.gameObject.SetActive(false);
        _pool.Enqueue(cube);
    }
}
