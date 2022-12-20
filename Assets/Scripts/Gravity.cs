using UnityEngine;

public class Gravity : MonoBehaviour
{
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
                var positionDifference = attractor.transform.position - attractedBody.transform.position;
                var distanceBetween = positionDifference.sqrMagnitude;
                var direction = positionDifference.normalized;
                totalForce += UniversalConstants.Gravity * attractor.Mass * direction / distanceBetween;
            }

            GravitationalForces[i] = totalForce;
            //Debug.Log($"{attractedBody.name}: {totalForce.magnitude}N in direction {totalForce.normalized}");
        }

        for (var i = 0; i < Bodies.Length; i++) Bodies[i].ApplyForce(GravitationalForces[i]);
    }
}