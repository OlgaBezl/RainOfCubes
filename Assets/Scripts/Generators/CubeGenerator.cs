using System.Collections;
using UnityEngine;

public class CubeGenerator : GenericGenerator<Cube>
{
    [SerializeField] private float _delay;
    [SerializeField] private PlayingField _playingField;

    private Coroutine _coroutine;

    protected override void Start()
    {
        base.Start();
        _coroutine = StartCoroutine(GenerateCrystals());
    }

    protected override void Reset()
    {
        base.Reset();
        StopCoroutine(_coroutine);
    }

    private IEnumerator GenerateCrystals()
    {
        var wait = new WaitForSeconds(_delay);

        while (IsActive)
        {
            Spawn();
            yield return wait;
        }
    }

    private void Spawn()
    {
        Cube cube = base.Spawn();
        cube.transform.position = _playingField.GetRandomPosition(transform.position.y);
    }
}
