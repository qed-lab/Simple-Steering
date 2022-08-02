using UnityEngine;

/// <summary>
/// Author: Diego Andino
/// 
/// This class represents a Pursue Steering behavior for agents.
/// </summary>
public class DynamicPursue : DynamicSeek
{
    // The maximum prediction time.
    public float MaxPrediction;

    // Used to reference Target and its AgentController component
    private GameObject _explicitTarget;
    private AgentController _targetAgent;

    public override void Awake()
    {
        base.Awake();
        _targetAgent = Target.GetComponent<AgentController>();
        _explicitTarget = Target;
        Target = new GameObject();
    }

    /// <summary>
    /// Generate a Steering object that is based on Dynamic Pursue rules as defined in AI for Games by Ian Millington. The behavior
    /// seeks to be at the same position as the explicit target at some point in the future, so it will opt to go to the potential location
    /// over the target's current actual location.
    /// </summary>
    /// <returns>A Steering object.</returns>
    public override Steering GetSteering()
    {
        // Calculate the Target to delegate to seek
        Vector3 direction = _explicitTarget.transform.position - transform.position;

        // Work out the distance to Target.
        float distance = direction.magnitude;

        // Work out our current speed
        float speed = Agent.Velocity.magnitude;

        /* Check if speed gives a reasonable prediction time.
         * Otherwise, calculate the prediction time.
        */
        float prediction;
        if (speed <= distance / MaxPrediction)
            prediction = MaxPrediction;
        else
            prediction = distance / speed;

        // Put the Target together.
        Target.transform.position = _explicitTarget.transform.position;
        Target.transform.position += _targetAgent.Velocity * prediction;

        // Delegate to seek.
        return base.GetSteering();
    }
}
