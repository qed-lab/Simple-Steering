using UnityEngine;

/// <summary>
/// Author: Diego Andino
/// 
/// This class represents an Evade Steering behavior for agents.
/// </summary>
public class DynamicEvade : DynamicFlee
{
    // The maximum prediction time.
    public float MaxPrediction;

    // Radius of the Agent; AgentController will Evade when Target enters radius
    public float PersonalSpaceRadius; 

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
    /// Generate a Steering object based on Dynamic Evade according to AI for Games by Ian Millington. This will attempt to avoid a single
    /// target if they enter the Agent's perceived personal space.
    /// </summary>
    /// <returns></returns>
    public override Steering GetSteering()
    {
        // Calculate the Target to delegate to seek
        Vector3 direction = _explicitTarget.transform.position - transform.position;

        // Work out the distance to Target.
        float distance = direction.magnitude;
        if (distance > PersonalSpaceRadius)
            return new Steering(); 

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

        // Delegate to flee.
        return base.GetSteering();
    }
}
