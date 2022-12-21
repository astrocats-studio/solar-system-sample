using UnityEngine;

[ExecuteInEditMode]
public class OrbitDebugDisplay : MonoBehaviour
{
    public int numSteps = 1000;

    private void Update()
    {
        if (!Application.isPlaying) DrawOrbits();
    }

    private void DrawOrbits()
    {
        var originalBodies = GetComponentsInChildren<Body>();
        var bodies = new VirtualBody[originalBodies.Length];
        var pointsToDraw = new Vector3[originalBodies.Length][];

        for (var i = 0; i < bodies.Length; i++)
        {
            bodies[i] = new VirtualBody(originalBodies[i]);
            pointsToDraw[i] = new Vector3[numSteps];
        }

        for (var step = 0; step < numSteps; step++)
        {
            for (var i = 0; i < bodies.Length; i++)
                bodies[i].Velocity += CalculateAcceleration(i, bodies);

            for (var i = 0; i < bodies.Length; i++)
            {
                var newPosition = bodies[i].Position + bodies[i].Velocity;
                bodies[i].Position = newPosition;
                pointsToDraw[i][step] = newPosition;
            }
        }

        for (var bodyIndex = 0; bodyIndex < bodies.Length; bodyIndex++)
        for (var i = 0; i < pointsToDraw[bodyIndex].Length - 1; i++)
            Debug.DrawLine(pointsToDraw[bodyIndex][i], pointsToDraw[bodyIndex][i + 1], Color.yellow);
    }

    private Vector3 CalculateAcceleration(int i, VirtualBody[] virtualBodies)
    {
        var totalForce = Vector3.zero;
        for (var j = 0; j < virtualBodies.Length; j++)
        {
            if (i == j) continue;

            var positionDifference = virtualBodies[j].Position - virtualBodies[i].Position;
            var distanceBetween = positionDifference.sqrMagnitude;
            var direction = positionDifference.normalized;
            totalForce += UniversalConstants.Gravity * virtualBodies[j].Mass * direction / distanceBetween;
        }

        return totalForce;
    }

    private class VirtualBody
    {
        public readonly float Mass;
        public Vector3 Position;
        public Vector3 Velocity;

        public VirtualBody(Body body)
        {
            Position = body.transform.position;
            Velocity = body.Velocity;
            Mass = body.GetComponent<Rigidbody>().mass;
        }
    }
}