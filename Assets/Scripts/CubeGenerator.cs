using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeGenerator : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private PlayingField _playingField;
    [SerializeField] private CubePool _pool;

    private bool _isActive;
    private Coroutine _coroutine;

    public void Start()
    {
        _isActive = true;
        _coroutine = StartCoroutine(GenerateCrystals());
    }

    public void Reset()
    {
        _isActive = false;
        StopCoroutine(_coroutine);
        _pool.Reset();
    }

    private IEnumerator GenerateCrystals()
    {
        var wait = new WaitForSeconds(_delay);

        while (_isActive)
        {
            Spawn();
            yield return wait;
        }
    }

    private void Spawn()
    {
        Cube cube = _pool.GetObject();
        cube.transform.position = _playingField.GetRandomPosition(transform.position.y);
    }
}
