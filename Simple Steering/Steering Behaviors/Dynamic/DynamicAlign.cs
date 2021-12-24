using UnityEngine;
using System.Collections;

/// <summary>
/// Author: Diego Andino
/// OpenFL Project 2021
/// 
/// This class represents an Align Steering behavior for agents.
/// </summary>
public class DynamicAlign : AgentBehaviour
{
    // The radius for arriving at the Target.
    public float TargetRadius;

    // The radius for beginning to slow down.
    public float SlowRadius;

    // The time over which to achieve Target speed.
    [System.NonSerialized]
    public float TimeToTarget = 0.1f;

    public override Steering GetSteering()
    {
        Steering steering = new Steering();

        // Get the naive direction to the Target.
        float targetOrientation = Target.GetComponent<AgentController>().Orientation;
        float rotation = targetOrientation - Agent.Orientation;

        //  Map the result to the (-pi, pi) interval.
        rotation = MapToRange(rotation);
        float rotationSize = Mathf.Abs(rotation);

        // Check if we are there, return no Steering.
        if (rotationSize < TargetRadius)
            return steering;

        /**
         * If we are outside the SlowRadius, then use maximum Rotation.
         * Otherwise calculate a scaled Rotation.
         **/ 
        float targetRotation;
        if (rotationSize > SlowRadius)
            targetRotation = Agent.MaxRotation;
        else
            targetRotation = Agent.MaxRotation * rotationSize / SlowRadius;

        // The final Target Rotation combines speed (already in the variable) and direction.
        targetRotation *= rotation / rotationSize;

        // Acceleration tries to get to the Target Rotation.
        steering.Angular = targetRotation - Agent.Rotation;
        steering.Angular /= TimeToTarget;

        // Check if the acceleration is too great.
        float angularAcceleration = Mathf.Abs(steering.Angular);
        if (angularAcceleration > Agent.MaxAngularAcceleration)
        {
            steering.Angular /= angularAcceleration;
            steering.Angular *= Agent.MaxAngularAcceleration;
        }

        return steering;
    }
}