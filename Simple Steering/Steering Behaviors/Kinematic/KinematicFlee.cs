using UnityEngine;
using System.Collections;

/// <summary>
/// Author: Diego Andino
/// OpenFL Project 2021
/// 
/// This class represents a Kinematic Flee Steering behavior for Kinematic agents.
/// </summary>
public class KinematicFlee : AgentBehaviour
{
    public override Steering GetSteering()
    {
        Steering result = new Steering();
        
        result.Linear = transform.position - Target.transform.position;
        LookWhereYoureGoing(result.Linear);
        result.Linear.Normalize();
        result.Linear *= Agent.MaxSpeed;

        return result;
    }
}