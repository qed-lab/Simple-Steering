using UnityEngine;

/// <summary>
/// Author: Diego Andino
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

    /// <summary>
    /// Generates a Steering object based on the Dynamic Face rule based on the work of AI for Games by Ian Millington. This steering
    /// behavior will attempt to make the Agent face a specific target.
    /// </summary>
    /// <returns>A Steering object.</returns>
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
