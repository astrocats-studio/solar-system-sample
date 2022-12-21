using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Body : MonoBehaviour
{
    public Vector3 Velocity = Vector3.zero;

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

    private void Update()
    {
        //transform.localScale.
    }

    public void ApplyForce(Vector3 acceleration)
    {
        Velocity += acceleration;
        var previousPosition = _rigidBody.position;
        var newPosition = previousPosition + Velocity;

        Debug.DrawLine(previousPosition, newPosition, _planetColor, 100000);
        _rigidBody.MovePosition(newPosition);
    }
}