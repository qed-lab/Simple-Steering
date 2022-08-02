/// <summary>
/// Author: Diego Andino
/// 
/// This class represents a Kinematic Flee Steering behavior for Kinematic agents.
/// </summary>
public class KinematicFlee : AgentBehaviour
{
    /// <summary>
    /// A simple Seek Steering behavior as defined AI for Games by Ian Millington. Does not rely on the physics engine,
    /// and simply just moves the position (away from the target) rather than moving based on velocity.
    /// </summary>
    /// <returns>Returns a Steering behavior.</returns>
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