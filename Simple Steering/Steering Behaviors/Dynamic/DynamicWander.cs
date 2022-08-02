using UnityEngine;

/// <summary>
/// Author: Diego Andino
/// 
/// This class represents a Wander Steering behavior for agents.
/// </summary>
public class DynamicWander : DynamicFace
{
    // The radius and forward offset of the wander circle.
    public float WanderOffset;
    public float WanderRadius;

    // The maximum rate at which the wander Orientation can change.
    public float WanderRate;

    public override void Awake()
    {
        Target = new GameObject();
        Target.transform.position = transform.position;
        base.Awake();
    }

    /// <summary>
    /// Generates a Steering behavior that is based on the Dynamic Wander rules in AI for Games by Ian Millington. Dynamic Wander picks a
    /// random point in a radius around the agent, then navigates towards it using either Seek (by default).
    /// </summary>
    /// <returns>A Steering object with relevant movemtn and rotation information.</returns>
    public override Steering GetSteering()
    {
        Steering steering = new Steering();

        // Update the wander Orientation.
        float wanderOrientation = Random.Range(-1.0f, 1.0f) * WanderRate;

        // Calculate the combined Target Orientation.
        float targetOrientation = wanderOrientation + Agent.Orientation;

        // Calculate the center of the wander circle.
        Vector3 agentOrientationVec = GetOrientationAsVector(Agent.Orientation);
        Vector3 targetPosition = (WanderOffset * agentOrientationVec) + transform.position;

        // Calculate the Target location.
        targetPosition += GetOrientationAsVector(targetOrientation) * WanderRadius;
        ExplicitTarget.transform.position = targetPosition;

        // Delegate to face.
        steering = base.GetSteering();

        // Now set the Linear acceleration to be at full sacceleration in the direction of the Orientation.
        steering.Linear = ExplicitTarget.transform.position - transform.position;
        steering.Linear.Normalize();
        steering.Linear *= Agent.MaxAcceleration;
        
        return steering;
    }
}
