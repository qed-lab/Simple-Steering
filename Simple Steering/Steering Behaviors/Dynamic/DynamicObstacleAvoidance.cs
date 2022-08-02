using UnityEngine;

/// <summary>
/// Author: Diego Andino
/// 
/// This class represents an Obstacle Avoidance Steering behavior for agents.
/// </summary>
public class DynamicObstacleAvoidance : DynamicArrive
{
    // The distance to look ahead for a collision.
    public float LookAhead;

    // The number of raycasted rays from the Agent for avoidance.
    public int NumberOfRays;

    // The amount of force applied when avoiding _targets.
    public int AvoidForce;

    // The angle that the array of rays will aim at. 
    private float _rayAngle = 90f;

    public override void Awake()
    {
        base.Awake();
        base.TargetRadius = 600f;
        base.SlowingRadius = 1000f;
    }

    /// <summary>
    /// Generates a Steering object based on the Dynamic Obstacle Avoidance rules based on AI for Games by Ian Millington.
    /// This will attempt to avoid obstacles such as walls or environmental hazards, assuming that the hitbox is circular.
    /// </summary>
    /// <returns>A Steering object.</returns>
    public override Steering GetSteering()
    {
        for (int i = 0; i < NumberOfRays; i++)
        {
            Vector3 direction = GetDirection(i);
            Ray ray = new Ray(transform.position, direction);
            if (Physics.Raycast(ray, LookAhead))
            {
                transform.position -= (1.0f / NumberOfRays) * AvoidForce * direction;
            }
            else
            {
                transform.position += (1.0f / NumberOfRays) * AvoidForce * direction; 
                LookWhereYoureGoing(direction);
            }
        }

        return base.GetSteering();
    }

    /// <summary>
    /// Helper function that returns the direction of raycast.
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    private Vector3 GetDirection(int index)
    {
        Quaternion currentRotation = transform.rotation;
        Quaternion rotationModifier = Quaternion.AngleAxis((index / (float)NumberOfRays - 1) * _rayAngle * 2 + _rayAngle, transform.up);
        return currentRotation * rotationModifier * Vector3.forward;
    }

    /// <summary>
    /// Helper Unity method to visualize the raycasts.
    /// </summary>
    void OnDrawGizmos()
    {
        for (int i = 0; i < NumberOfRays; i++)
            Gizmos.DrawRay(transform.position, GetDirection(i) * LookAhead);
    }
}