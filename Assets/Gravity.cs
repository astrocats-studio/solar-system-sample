using UnityEngine;

public class Gravity : MonoBehaviour
{
    private readonly float GravityConstant = 0.0001f;
    private Body[] Bodies;
    private Vector3[] GravitationalForces;

    private void Start()
    {
        Bodies = GetComponentsInChildren<Body>();
        GravitationalForces = new Vector3[Bodies.Length];
    }

    private void FixedUpdate()
    {
        for (var i = 0; i < Bodies.Length; i++)
        {
            var totalForce = Vector3.zero;
            var attractedBody = Bodies[i];
            foreach (var attractor in Bodies)
            {
                if (attractor == attractedBody) continue;
                var attractorPosition = attractor.transform.position;
                var attractedPosition = attractedBody.transform.position;
                var distanceBetween = (attractorPosition - attractedPosition).sqrMagnitude;
                var direction = (attractorPosition - attractedPosition).normalized;
                totalForce += GravityConstant * attractor.Mass * direction / distanceBetween;
            }

            GravitationalForces[i] = totalForce;
            Debug.Log($"{attractedBody.name}: {totalForce.magnitude}N in direction {totalForce.normalized}");
        }

        for (var i = 0; i < Bodies.Length; i++) Bodies[i].ApplyForce(GravitationalForces[i]);
    }
}