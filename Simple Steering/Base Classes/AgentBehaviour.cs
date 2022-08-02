using UnityEngine;

/// <summary>
/// Author: Diego Andino
/// 
/// This is a base class that represents AgentBehavior for Agents using Steering Behaviors.
/// </summary>
public class AgentBehaviour : MonoBehaviour
{
    public GameObject Target;
    protected AgentController Agent;

    public virtual void Awake()
    {
        Agent = gameObject.GetComponent<AgentController>();
    }

    public virtual void Update()
    {
        Agent.SetSteering(GetSteering());
    }

    /// <summary>
    /// Returns the calculated Steering behavior from derived classes.
    /// </summary>
    /// <returns>A Steering result.</returns>
    public virtual Steering GetSteering()
    {
        return new Steering();
    }

    /// <summary>
    /// Returns a new forward Orientation towards Target
    /// </summary>
    /// <param name="current"></param>
    /// <param name="velocity"></param>
    /// <returns></returns>
    public float NewOrientation(float current, Vector3 velocity)
    {
        if (velocity.magnitude > 0)
            return Mathf.Atan2(-Target.transform.eulerAngles.x, Target.transform.eulerAngles.z);

        return current; 
    }

    /// <summary>
    /// Maps a rotation to between 180 degrees both positive and negative.
    /// </summary>
    /// <param name="rotation"></param>
    /// <returns>The clamped rotation.</returns>
    public float MapToRange(float rotation)
    {
        rotation %= 360.0f;
        if (Mathf.Abs(rotation) > 180.0f)
        {
            if (rotation < 0.0f)
                rotation += 360.0f;
            else
                rotation -= 360.0f;
        }

        return rotation;
    }

    /// <summary>
    /// Converts given Orientation value (in degrees) to a vector (in radians)
    /// </summary>
    /// <param name="orientation"></param>
    /// <returns></returns>
    public Vector3 GetOrientationAsVector(float orientation)
    {
        Vector3 vector = Vector3.zero;
        vector.x = Mathf.Sin(orientation * Mathf.Deg2Rad) * 1.0f;
        vector.z = Mathf.Cos(orientation * Mathf.Deg2Rad) * 1.0f;
        
        return vector.normalized;
    }

    /// <summary>
    /// Clamps Agent Velocity towards Target so Agent is looking in the correct direction. 
    /// </summary>
    /// <param name="desiredVelocity"></param>
    /// <returns> A Vector3 containing calculated Steering </returns>
    public void LookWhereYoureGoing(Vector3 desiredVelocity)
    {
        Vector3 steering = desiredVelocity - Agent.GetComponent<Rigidbody>().velocity;
        steering = Vector3.ClampMagnitude(steering, Agent.MaxAcceleration);
        steering /= Agent.GetComponent<Rigidbody>().mass;

        desiredVelocity = Vector3.ClampMagnitude(Agent.Velocity + steering, Agent.MaxSpeed);
        Agent.transform.forward = desiredVelocity.normalized;
    }
}