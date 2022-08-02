using UnityEngine;

/// <summary>
/// Author: Diego Andino
/// 
/// This class represents an Arrive Steering behavior for agents.
/// </summary>
public class DynamicArrive : AgentBehaviour
{
    // The radius for arriving at the Target.
    public float TargetRadius;

    // The radius for beginning to slow down.
    public float SlowingRadius;

    // The time over which to achieve Target speed.
    private const float _timeToTarget = 0.1f;

    /// <summary>
    /// Generates a Steering result based on the details in AI for Games by Ian Millington. This will generate a Steering result similar to
    /// the DynamicSeek UNTIL it approaches the end, where it will slow down and try to arrive right on the end point (or within a
    /// given radius).
    /// </summary>
    /// <returns>A Steering result.</returns>
    public override Steering GetSteering()
    {
        Steering steering = new Steering();

        // Get the direction to the Target and point in the right direction.
        Vector3 direction = Target.transform.position - transform.position;
        LookWhereYoureGoing(direction);
        float distance = direction.magnitude;

        // Check if we are there, return no Steering.
        if (distance < TargetRadius)
            return steering;

        /** 
         * If we are outside the SlowRadius, then move at max speed
         * Otherwise calculate a scaled speed.
         */
        float targetSpeed;
        if (distance > SlowingRadius)
            targetSpeed = Agent.MaxSpeed;
        else
            targetSpeed = Agent.MaxSpeed * distance / SlowingRadius;

        // The Target Velocity combines speed and direction.
        Vector3 desiredVelocity = direction;
        desiredVelocity.Normalize();
        desiredVelocity *= targetSpeed;

        // Acceleration tries to get to the Target Velocity.
        steering.Linear = desiredVelocity - Agent.Velocity;
        steering.Linear /= _timeToTarget;

        // Check if the acceleration is too fast.
        if (steering.Linear.magnitude > Agent.MaxAcceleration)
        {
            steering.Linear.Normalize();
            steering.Linear *= Agent.MaxAcceleration;
        }

        steering.Angular = 0;
        return steering;
    }
}