/// <summary>
/// Author: Diego Andino
/// 
/// This class represents a Velocity Match Steering behavior for agents.
/// </summary>
public class DynamicVelocityMatch : AgentBehaviour
{
    public float MaxAcceleration;

    // The time over which to achieve Target speed.
    private const float _timeToTarget = 0.1f;

    /// <summary>
    /// Generates a Steering object that will match the velocity of a specifc target as defined in AI for Games by Ian Millington.
    /// This acts similar to Face, Align, Seek, and Arrive but instead of affecting just position or orientation, it attempts to match
    /// the velocity of a given target.
    /// </summary>
    /// <returns></returns>
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
