using UnityEngine;

public class Body : MonoBehaviour
{
    private const float SpeedMultiplier = 1;

    public float Mass = 10;
    public Vector3 Velocity = Vector3.zero;

    private Vector3 Acceleration = Vector3.zero;
    private Color PlanetColor;

    private void Awake()
    {
        PlanetColor = Random.ColorHSV();
        GetComponent<MeshRenderer>().material.color = PlanetColor;
    }

    private void FixedUpdate()
    {
        var previousPosition = transform.position;
        var time = Time.fixedDeltaTime * SpeedMultiplier;
        Velocity += Acceleration * time;
        var newPosition = Velocity * time + previousPosition;

        Debug.DrawLine(previousPosition, newPosition, PlanetColor, 100000);
        transform.position = newPosition;
    }

    public void ApplyForce(Vector3 force)
    {
        Acceleration = force / Mass;
    }
}