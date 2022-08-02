/// <summary>
/// Author: Diego Andino
/// 
/// This class represents a Kinematic Seek Steering behavior for Kinematic agents.
/// </summary>
public class KinematicSeek : AgentBehaviour
{
    /// <summary>
    /// A simple Seek Steering behavior as defined AI for Games by Ian Millington. Does not rely on the physics engine,
    /// and simply just moves the position (towards the target) rather than moving based on velocity.
    /// </summary>
    /// <returns></returns>
    public override Steering GetSteering()
    {
        Steering result = new Steering(); 

        result.Linear = Target.transform.position - transform.position;
        LookWhereYoureGoing(result.Linear);
        result.Linear.Normalize();
        result.Linear *= Agent.MaxSpeed;

        return result;
    }
}
