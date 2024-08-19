using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bomb : MaterialObject, IPoolItem<Bomb>
{
    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _dissolveStep = 0.001f;

    private float _alphaValue;
    private Coroutine _coroutine;

    public event Action<Bomb> Destroyed;

    public void Activate()
    {
        _alphaValue = 1f;
        gameObject.SetActive(true);

        _coroutine = StartCoroutine(WaitDissolve());
    }
   
    private IEnumerator WaitDissolve()
    {
        while (_alphaValue > 0)
        {
            Dissolve(); 
            yield return null;
        }

        ExplodeWithShockwave();
        Destroyed?.Invoke(this);
        StopCoroutine(_coroutine);
    }

    private void Dissolve()
    {
        _alphaValue -= _dissolveStep;
        Renderer.material.color = new Color(0, 0, 0, _alphaValue);
    }

    private void ExplodeWithShockwave()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _explosionRadius);

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent(out MaterialObject materialObject))
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                if (distance == 0) continue;

                float finalExplosionForce = _explosionForce / distance;
                materialObject.AddExplosionForce(finalExplosionForce, transform.position, _explosionRadius);
            }
        }
    }
}
