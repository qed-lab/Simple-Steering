using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Diego Andino
/// OpenFL Project 2021
/// 
/// This class represents a Velocity Match Steering behavior for agents.
/// </summary>
public class DynamicVelocityMatch : AgentBehaviour
{
    public float MaxAcceleration;

    // The time over which to achieve Target speed.
    private const float _timeToTarget = 0.1f; 

    public override Steering GetSteering()
    {
        Steering steering = new Steering();

        // Acceleration tries to get to the Target Velocity.
        steering.Linear = Target.GetComponent<AgentController>().Velocity - Agent.Velocity;
        steering.Linear /= _timeToTarget;

        // Check if the acceleration is too fast.
        if (steering.Linear.magnitude > MaxAcceleration) {
            steering.Linear.Normalize();
            steering.Linear *= MaxAcceleration;
        }

        steering.Angular = 0; 
        return steering; 
    }
}
