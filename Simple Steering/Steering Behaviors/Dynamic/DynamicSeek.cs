/// <summary>
/// Author: Diego Andino
/// 
/// This class represents a Dynamic Seek Steering behavior for agents.
/// </summary>
public class DynamicSeek : AgentBehaviour
{
    /// <summary>
    /// Return a Steering result based on the Dynamic Seek method as defined in AI for Games by Ian Millington. Move
    /// from one position to the next at maximum speed.
    /// </summary>
    /// <returns>A Steering object.</returns>
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