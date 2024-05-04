using UnityEngine;

public class PlayingField : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;

    private float _halfWidth;
    private float _halfDepth;
    private float _halfFactor = 0.5f;

    private void Start()
    {
        Vector3 size = _meshRenderer.bounds.size;
        _halfWidth = size.x * _halfFactor;
        _halfDepth = size.z * _halfFactor;
    }

    public Vector3 GetRandomPosition(float positionY)
    {
        float positionX = Random.Range(-_halfWidth, _halfWidth);
        float positionZ = Random.Range(-_halfDepth, _halfDepth);

        return new Vector3(positionX, positionY, positionZ);
    }
}
