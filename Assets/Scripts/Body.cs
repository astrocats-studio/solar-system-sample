using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Body : MonoBehaviour
{
    public Vector3 Velocity = Vector3.zero;

    private Vector3 _acceleration = Vector3.zero;
    private Color _planetColor;
    private Rigidbody _rigidBody;
    public float Mass { get; private set; }

    private void Awake()
    {
        _planetColor = Random.ColorHSV();
        GetComponent<MeshRenderer>().material.color = _planetColor;
        _rigidBody = GetComponent<Rigidbody>();
        Mass = _rigidBody.mass;
    }

    public void ApplyForce(Vector3 force)
    {
        _acceleration = force / Mass;
        var previousPosition = _rigidBody.position;
        var time = Time.fixedDeltaTime * UniversalConstants.SpeedMultiplier;
        Velocity += _acceleration * time;
        var newPosition = Velocity * time + previousPosition;

        Debug.DrawLine(previousPosition, newPosition, _planetColor, 100000);
        _rigidBody.MovePosition(newPosition);
    }
}