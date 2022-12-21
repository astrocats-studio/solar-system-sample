using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Rigidbody))]
public class Body : MonoBehaviour
{
    public Vector3 Velocity = Vector3.zero;

    public float Radius = 1f;
    public float SurfaceGravity = 9.8f;
    private Rigidbody _rb;

    private Color _surfaceColor;
    public float Mass => _rigidBody.mass;

    private Rigidbody _rigidBody
    {
        get
        {
            if (!_rb) _rb = GetComponent<Rigidbody>();
            return _rb;
        }
    }

    private void Awake()
    {
        _surfaceColor = Random.ColorHSV();
        GetComponent<MeshRenderer>().sharedMaterial.color = _surfaceColor;
        UpdateMassAndScale();
    }

    private void OnValidate()
    {
        UpdateMassAndScale();
    }

    private void UpdateMassAndScale()
    {
        _rigidBody.mass = SurfaceGravity * Radius * Radius / UniversalConstants.Gravity;
        transform.localScale = Vector3.one * Radius;
    }

    public void ApplyForce(Vector3 acceleration)
    {
        Velocity += acceleration;
        var previousPosition = _rigidBody.position;
        var newPosition = previousPosition + Velocity;

        Debug.DrawLine(previousPosition, newPosition, _surfaceColor, 100000);
        _rigidBody.MovePosition(newPosition);
    }
}