using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Diego Andino
/// OpenFL Project 2021
/// 
/// This class represents a Face Steering behavior for agents.
/// </summary>
public class DynamicFace : DynamicAlign
{
    protected GameObject ExplicitTarget;

    public override void Awake()
    {
        base.Awake();

        ExplicitTarget = Target;
        Target = new GameObject();
        Target.AddComponent<AgentController>();
    }

    public override Steering GetSteering()
    {
        // Work out the direction to Target.
        Vector3 direction = ExplicitTarget.transform.position - transform.position;
        Debug.Log(direction); 

        // Check for a zero direction, and make no change if so.
        if (direction.magnitude > 0.0f)
        {
            float targetOrientation = Mathf.Atan2(direction.x, direction.z);

            // Convert back to Degrees since Unity works in degrees
            targetOrientation *= Mathf.Rad2Deg;
            Target.GetComponent<AgentController>().Orientation = targetOrientation;
        }

        return base.GetSteering();
    }
}
