using UnityEngine;

public class PlayingField : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;

    private float _halfWidth;
    private float _halfDepth;

    private void Start()
    {
        Vector3 size = _meshRenderer.bounds.size;
        _halfWidth = size.x / 2f;
        _halfDepth = size.z / 2f;
    }

    public Vector3 GetRandomPosition(float positionY)
    {
        float positionX = Random.Range(-_halfWidth, _halfWidth);
        float positionZ = Random.Range(-_halfDepth, _halfDepth);

        return new Vector3(positionX, positionY, positionZ);
    }
}
