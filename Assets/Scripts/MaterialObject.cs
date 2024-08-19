using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class MaterialObject : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public Color DefaultColor { get; private set; }
    public MeshRenderer Renderer { get; private set; }

    private void Awake()
    {
        Renderer = GetComponent<MeshRenderer>();
        DefaultColor = Renderer.material.color;
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void AddExplosionForce(float force, Vector3 position, float radius)
    {
        _rigidbody.AddExplosionForce(force, position, radius);
    }
}
