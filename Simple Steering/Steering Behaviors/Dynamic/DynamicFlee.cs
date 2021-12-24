using UnityEngine;
using System.Collections;

/// <summary>
/// Author: Diego Andino
/// OpenFL Project 2021
/// 
/// This class represents a Dynamic Flee Steering behavior for agents.
/// </summary>
public class DynamicFlee : AgentBehaviour
{
    public override Steering GetSteering()
    {
        Steering result = new Steering();
        
        result.Linear = transform.position - Target.transform.position;
        LookWhereYoureGoing(result.Linear);
        result.Linear.Normalize();
        result.Linear *= Agent.MaxAcceleration;

        return result;
    }
}