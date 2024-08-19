using UnityEngine;

public class CubePool : GenericPool<Cube>
{
    [SerializeField] private BombGenerator _bombGenerator;

    private void OnEnable()
    {
        ItemWasDestroyed += _bombGenerator.Spawn;
    }

    private void OnDisable()
    {
        ItemWasDestroyed -= _bombGenerator.Spawn;
    }
}
