using UnityEngine;

public class BombGenerator: GenericGenerator<Bomb>
{
    public void Spawn(Vector3 position)
    {
        Bomb bomb = base.Spawn();
        bomb.transform.position = position;
    }
}
