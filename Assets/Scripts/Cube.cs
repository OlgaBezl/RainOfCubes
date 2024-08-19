using System;
using System.Collections;
using UnityEngine;

public class Cube : MaterialObject, IPoolItem<Cube>
{
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;

    private bool _isTouched;
    private Coroutine _coroutine;

    public event Action<Cube> Destroyed;

    private void OnCollisionEnter(Collision collision)
    {
        if (_isTouched == false && collision.gameObject.TryGetComponent(out PlayingField _))
        {
            _isTouched = true;
            RandomChangeColor();
            SetRandomLifeTime();
        }
    }

    public void Activate()
    {
        _isTouched = false;
        Renderer.material.color = DefaultColor;
        gameObject.SetActive(true);
    }

    private void RandomChangeColor()
    {
        Renderer.material.color = UnityEngine.Random.ColorHSV();
    }

    private void SetRandomLifeTime()
    {
        float lifeTime = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime + 1);
        _coroutine = StartCoroutine(WaitDestroy(lifeTime));
    }

    private IEnumerator WaitDestroy(float lifeTime)
    {
        bool wait = true;

        while (wait)
        {
            wait = false;
            yield return new WaitForSeconds(lifeTime);
        }

        Destroyed?.Invoke(this);
        StopCoroutine(_coroutine);
    }
}
