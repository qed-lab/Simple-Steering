using UnityEngine;
using System.Collections;

/// <summary>
/// Author: Diego Andino
/// OpenFL Project 2021
/// 
/// This class represents a Dynamic Seek Steering behavior for agents.
/// </summary>
public class DynamicSeek : AgentBehaviour
{
    public override Steering GetSteering()
    {
        Steering result = new Steering(); 

        result.Linear = Target.transform.position - transform.position;
        LookWhereYoureGoing(result.Linear);
        result.Linear.Normalize(); 
        result.Linear *= Agent.MaxAcceleration;

        return result;
    }
}