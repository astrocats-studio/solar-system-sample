using UnityEngine;

public class Gravity : MonoBehaviour
{
    private Body[] _bodies;
    private Vector3[] _gravitationalForces;

    private void Start()
    {
        _bodies = GetComponentsInChildren<Body>();
        _gravitationalForces = new Vector3[_bodies.Length];
    }

    private void FixedUpdate()
    {
        for (var i = 0; i < _bodies.Length; i++)
        {
            var totalForce = Vector3.zero;
            var attractedBody = _bodies[i];
            foreach (var attractor in _bodies)
            {
                if (attractor == attractedBody) continue;
                var positionDifference = attractor.transform.position - attractedBody.transform.position;
                var distanceBetween = positionDifference.sqrMagnitude;
                var direction = positionDifference.normalized;
                totalForce += UniversalConstants.Gravity * attractor.Mass * direction / distanceBetween;
            }

            _gravitationalForces[i] = totalForce;
        }

        for (var i = 0; i < _bodies.Length; i++) _bodies[i].ApplyForce(_gravitationalForces[i]);
    }
}